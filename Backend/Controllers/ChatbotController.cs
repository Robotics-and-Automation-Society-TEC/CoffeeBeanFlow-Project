using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace CoffeeBeanFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly ILogger<ChatbotController> _logger;

        public ChatbotController(ILogger<ChatbotController> logger)
        {
            _logger = logger;
        }

        private static string GetPythonPath()
        {
            var envPath = Environment.GetEnvironmentVariable("CHATBOT_PYTHON_PATH");
            if (!string.IsNullOrWhiteSpace(envPath))
            {
                return envPath;
            }

            var projectRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), ".."));
            var venvPython = Path.Combine(projectRoot, "venv", "bin", "python");
            if (System.IO.File.Exists(venvPython))
            {
                return venvPython;
            }

            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "python" : "python3";
        }

        public class ChatRequest
        {
            public string Message { get; set; } = "";
        }

        public class ChatResponse
        {
            public bool Success { get; set; }
            public string Response { get; set; } = "";
            public string? Error { get; set; }
        }

        // POST: api/Chatbot/ask
        [HttpPost("ask")]
        public async Task<ActionResult<ChatResponse>> Ask([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new ChatResponse
                {
                    Success = false,
                    Error = "El mensaje no puede estar vacío"
                });
            }

            var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "coffee_agent.py");
            var workingDir = Path.GetDirectoryName(scriptPath) ?? Directory.GetCurrentDirectory();
            var pythonPath = GetPythonPath();
            if (!System.IO.File.Exists(scriptPath))
            {
                return NotFound(new ChatResponse
                {
                    Success = false,
                    Error = $"No se encontró el archivo: {scriptPath}"
                });
            }

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = workingDir
                };
                psi.Environment["PYTHONUNBUFFERED"] = "1";
                psi.ArgumentList.Add(scriptPath);
                psi.ArgumentList.Add("--query");
                psi.ArgumentList.Add(request.Message);

                using var process = new Process { StartInfo = psi };
                var output = new StringBuilder();
                var error = new StringBuilder();

                process.OutputDataReceived += (_, e) => { if (e.Data != null) output.AppendLine(e.Data); };
                process.ErrorDataReceived += (_, e) => { if (e.Data != null) error.AppendLine(e.Data); };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                var waitTask = process.WaitForExitAsync();
                var completed = await Task.WhenAny(waitTask, Task.Delay(TimeSpan.FromSeconds(180)));
                if (completed != waitTask)
                {
                    try { process.Kill(); } catch { }
                    return StatusCode(500, new ChatResponse
                    {
                        Success = false,
                        Error = "Tiempo de espera agotado al ejecutar el chatbot"
                    });
                }

                if (process.ExitCode != 0)
                {
                    return StatusCode(500, new ChatResponse
                    {
                        Success = false,
                        Error = error.ToString().Trim()
                    });
                }

                var finalOutput = output.ToString().Trim();
                if (string.IsNullOrWhiteSpace(finalOutput))
                {
                    return StatusCode(500, new ChatResponse
                    {
                        Success = false,
                        Error = string.IsNullOrWhiteSpace(error.ToString())
                            ? "El chatbot no devolvió respuesta"
                            : error.ToString().Trim()
                    });
                }

                return Ok(new ChatResponse
                {
                    Success = true,
                    Response = finalOutput
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el chatbot");
                return StatusCode(500, new ChatResponse
                {
                    Success = false,
                    Error = ex.Message
                });
            }
        }

        // POST: api/Chatbot/start
        [HttpPost("start")]
        public ActionResult<object> StartChatbot()
        {
            try
            {
                var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "coffee_agent.py");
                var workingDir = Path.GetDirectoryName(scriptPath) ?? Directory.GetCurrentDirectory();
                var pythonPath = GetPythonPath();

                // Verificar que el archivo existe
                if (!System.IO.File.Exists(scriptPath))
                {
                    return NotFound(new { 
                        success = false, 
                        message = $"No se encontró el archivo: {scriptPath}" 
                    });
                }

                // Verificar sesión gráfica en Linux
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    var display = Environment.GetEnvironmentVariable("DISPLAY");
                    var wayland = Environment.GetEnvironmentVariable("WAYLAND_DISPLAY");
                    if (string.IsNullOrWhiteSpace(display) && string.IsNullOrWhiteSpace(wayland))
                    {
                        return Ok(new
                        {
                            success = false,
                            message = $"No hay sesión gráfica disponible (DISPLAY/WAYLAND_DISPLAY vacío). Abre una terminal y ejecuta:\ncd {workingDir}\n{pythonPath} coffee_agent.py"
                        });
                    }
                }

                Process? process = null;
                string terminalUsed = "";

                // Detectar sistema operativo y usar el comando apropiado
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // Windows - usar cmd
                    process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/k cd /d \"{workingDir}\" && \"{pythonPath}\" coffee_agent.py",
                            UseShellExecute = true,
                            CreateNoWindow = false
                        }
                    };
                    terminalUsed = "cmd.exe";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    // macOS - usar Terminal.app
                    var command = $"cd '{workingDir}' && '{pythonPath}' coffee_agent.py";
                    var escapedCommand = command.Replace("'", "'\\''");
                    
                    process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "osascript",
                            Arguments = $"-e 'tell application \"Terminal\" to do script \"{escapedCommand}\"'",
                            UseShellExecute = true,
                            CreateNoWindow = false
                        }
                    };
                    terminalUsed = "Terminal.app";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    var dbus = Environment.GetEnvironmentVariable("DBUS_SESSION_BUS_ADDRESS");

                    // Linux - intentar varias terminales (priorizar las que no requieren DBus)
                    var terminals = string.IsNullOrWhiteSpace(dbus)
                        ? new[]
                        {
                            new { Name = "xterm", Args = $"-hold -e bash -c 'cd \"{workingDir}\" && \"{pythonPath}\" coffee_agent.py'" },
                            new { Name = "x-terminal-emulator", Args = $"-e bash -c 'cd \"{workingDir}\" && \"{pythonPath}\" coffee_agent.py; exec bash'" },
                            new { Name = "xfce4-terminal", Args = $"--hold -e bash -c 'cd \"{workingDir}\" && \"{pythonPath}\" coffee_agent.py'" }
                        }
                        : new[]
                        {
                            new { Name = "gnome-terminal", Args = $"-- bash -c 'cd \"{workingDir}\" && \"{pythonPath}\" coffee_agent.py; exec bash'" },
                            new { Name = "xterm", Args = $"-hold -e bash -c 'cd \"{workingDir}\" && \"{pythonPath}\" coffee_agent.py'" },
                            new { Name = "x-terminal-emulator", Args = $"-e bash -c 'cd \"{workingDir}\" && \"{pythonPath}\" coffee_agent.py; exec bash'" },
                            new { Name = "konsole", Args = $"--hold -e bash -c 'cd \"{workingDir}\" && \"{pythonPath}\" coffee_agent.py'" },
                            new { Name = "xfce4-terminal", Args = $"--hold -e bash -c 'cd \"{workingDir}\" && \"{pythonPath}\" coffee_agent.py'" }
                        };

                    foreach (var terminal in terminals)
                    {
                        try
                        {
                            process = new Process
                            {
                                StartInfo = new ProcessStartInfo
                                {
                                    FileName = terminal.Name,
                                    Arguments = terminal.Args,
                                    UseShellExecute = true,
                                    CreateNoWindow = false
                                }
                            };

                            terminalUsed = terminal.Name;
                            break;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }

                if (process != null && string.IsNullOrEmpty(terminalUsed) == false)
                {
                    bool started = false;
                    try
                    {
                        started = process.Start();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al abrir la terminal");
                    }

                    if (started)
                    {
                        _logger.LogInformation($"Chatbot iniciado usando {terminalUsed}");
                        return Ok(new
                        {
                            success = true,
                            message = $"Chatbot iniciado en nueva terminal ({terminalUsed}). Escribe tus preguntas allí."
                        });
                    }
                }

                // Si ninguna terminal funcionó
                var pythonCmd = pythonPath;
                return Ok(new
                {
                    success = false,
                    message = $"No se pudo abrir terminal automáticamente. Abre una terminal y ejecuta:\ncd {workingDir}\n{pythonCmd} coffee_agent.py"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al iniciar el chatbot");
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error: {ex.Message}"
                });
            }
        }

        // GET: api/Chatbot/status
        [HttpGet("status")]
        public ActionResult<object> GetStatus()
        {
            return Ok(new
            {
                message = "Servicio de chatbot disponible"
            });
        }
    }
}
