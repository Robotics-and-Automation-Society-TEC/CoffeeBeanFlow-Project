@echo off
chcp 65001 > nul
echo ========================================
echo   LLENADO DE BASE DE DATOS - CoffeeBeanFlow
echo ========================================
echo.

set PGPASSWORD=1234
set PGHOST=localhost
set PGPORT=5432
set PGUSER=postgres
set PGDATABASE=coffeebeanflow_db

echo [1/7] Limpiando base de datos...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c "TRUNCATE TABLE rondas, catacion, rc_sobremaduras, rc_inmaduras, rc_maduras, formulario_caracterizacion, peso_verde, trilla, guardar_cafe, bodega, ncama, temperatura_secado, termo_higrometria, humedad, secado, area_acopio CASCADE;"

echo [2/7] Insertando 5 lotes en Area de Acopio...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO area_acopio (nlote, altura, zona, nrecibo, nproductor, nfinca, robjetivo, rtotal, vendido, disponible, enproceso, semilavado, \"natural\", anaerobico, miel, lavado, otro, pf_pulpa_pergamino, pf_dmecanicos, pf_segundas, pf_pergamino_pulpa, pdensidad_fruta, pdensidad_pergamino_humedo) VALUES ^
('LOTE-001', 1500, 'Zona Norte', 12345, 'José Pérez', 'La Esperanza', 85.5, 88.2, false, 1000, 'En proceso', false, true, false, false, false, NULL, 15.5, 2.3, 1.8, 12.4, 1.05, 0.85), ^
('LOTE-002', 1600, 'Zona Sur', 12346, 'María González', 'El Paraíso', 82.0, 84.5, false, 1200, 'En proceso', true, false, false, false, false, NULL, 14.2, 2.1, 1.5, 11.8, 1.08, 0.88), ^
('LOTE-003', 1450, 'Zona Este', 12347, 'Carlos Ramírez', 'Villa Nueva', 87.3, 89.1, false, 800, 'En proceso', false, false, true, false, false, NULL, 16.1, 1.9, 1.2, 13.5, 1.12, 0.92), ^
('LOTE-004', 1700, 'Zona Oeste', 12348, 'Ana Torres', 'San Miguel', 80.5, 83.8, false, 950, 'En proceso', false, false, false, true, true, NULL, 15.8, 2.5, 1.6, 12.9, 1.06, 0.86), ^
('LOTE-005', 1550, 'Zona Centro', 12349, 'Luis Mendoza', 'La Colina', 86.0, 88.5, false, 1100, 'En proceso', false, true, false, false, false, NULL, 14.9, 2.2, 1.4, 12.1, 1.09, 0.89);"

echo [3/7] Insertando registros de Secado con entidades débiles...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO secado (nlote, finicio, ffinal, dsecado) VALUES ^
('LOTE-001', '2025-12-10 08:00:00', '2025-12-12 18:00:00', 2), ^
('LOTE-002', '2025-12-11 09:00:00', '2025-12-13 17:00:00', 2), ^
('LOTE-003', '2025-12-12 07:30:00', '2025-12-14 16:30:00', 2);"

echo Insertando mediciones de Humedad...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO humedad (id_secado, humedad, fecha) SELECT id_secado, 45.5, '2025-12-10 12:00:00'::timestamp FROM secado WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_secado, 35.2, '2025-12-11 12:00:00'::timestamp FROM secado WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_secado, 22.8, '2025-12-12 12:00:00'::timestamp FROM secado WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_secado, 48.1, '2025-12-11 13:00:00'::timestamp FROM secado WHERE nlote='LOTE-002' UNION ALL ^
 SELECT id_secado, 38.5, '2025-12-12 13:00:00'::timestamp FROM secado WHERE nlote='LOTE-002' UNION ALL ^
 SELECT id_secado, 25.3, '2025-12-13 13:00:00'::timestamp FROM secado WHERE nlote='LOTE-002';"

echo Insertando mediciones de Termo-Higrometría...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO termo_higrometria (id_secado, temperatura_ambiente, humedad_ambiente, fecha) SELECT id_secado, 28.5, 65.2, '2025-12-10 14:00:00'::timestamp FROM secado WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_secado, 29.1, 62.8, '2025-12-11 14:00:00'::timestamp FROM secado WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_secado, 27.8, 68.1, '2025-12-11 15:00:00'::timestamp FROM secado WHERE nlote='LOTE-002' UNION ALL ^
 SELECT id_secado, 28.9, 64.5, '2025-12-12 15:00:00'::timestamp FROM secado WHERE nlote='LOTE-002';"

echo Insertando temperaturas de Secado...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO temperatura_secado (id_secado, temperatura, fecha) SELECT id_secado, 42.5, '2025-12-10 16:00:00'::timestamp FROM secado WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_secado, 45.2, '2025-12-11 16:00:00'::timestamp FROM secado WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_secado, 48.8, '2025-12-12 16:00:00'::timestamp FROM secado WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_secado, 43.1, '2025-12-11 17:00:00'::timestamp FROM secado WHERE nlote='LOTE-002' UNION ALL ^
 SELECT id_secado, 46.5, '2025-12-12 17:00:00'::timestamp FROM secado WHERE nlote='LOTE-002';"

echo Insertando número de camas...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO ncama (id_secado, numero_cama) SELECT id_secado, 'CAMA-A1' FROM secado WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_secado, 'CAMA-A2' FROM secado WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_secado, 'CAMA-B1' FROM secado WHERE nlote='LOTE-002' UNION ALL ^
 SELECT id_secado, 'CAMA-B2' FROM secado WHERE nlote='LOTE-002';"

echo [4/7] Insertando registros de Bodega...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO bodega (nlote, finicio_reposo, hinicial, hfinal, w_bellota, w_pergamino, d_bellota, d_pergamino, cantidad_sacos) VALUES ^
('LOTE-001', '2025-12-13 10:00:00', 12.5, 11.8, 450.0, 500.0, 0.85, 0.78, 10), ^
('LOTE-002', '2025-12-14 11:00:00', 13.0, 12.2, 520.0, 580.0, 0.86, 0.79, 12);"

echo Insertando relaciones GuardarCafe (Bodega-Secado)...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO guardar_cafe (id_bodega, id_secado, cantidad_sacos) SELECT b.id_bodega, s.id_secado, 10 FROM bodega b, secado s WHERE b.nlote='LOTE-001' AND s.nlote='LOTE-001' UNION ALL ^
 SELECT b.id_bodega, s.id_secado, 12 FROM bodega b, secado s WHERE b.nlote='LOTE-002' AND s.nlote='LOTE-002';"

echo [5/7] Insertando registros de Trilla con Peso Verde...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO trilla (nlote, ffinal_reposo, hinicial, hfinal, pprimera, psegundas, pcaracolillo, pmadres, pmenudos, rfinal_pelado, rfinal_seleccion, wverde_final) VALUES ^
('LOTE-001', '2025-12-14 08:00:00', 12.0, 11.0, 78.5, 8.2, 3.5, 2.1, 1.5, 85.5, 83.0, 850.0), ^
('LOTE-002', '2025-12-14 09:00:00', 12.5, 11.2, 76.8, 9.1, 3.8, 2.3, 1.7, 84.0, 81.5, 1000.0);"

echo Insertando Peso Verde...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO peso_verde (id_peso_verde, winferiores, wfinal, wfinal_inferiores) SELECT id_trilla, 145.0, 855.0, 145.0 FROM trilla WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_trilla, 192.0, 1008.0, 192.0 FROM trilla WHERE nlote='LOTE-002';"
echo [6/7] Insertando registros de Caracterización con RC...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO formulario_caracterizacion (tiempo, nlote_area_acopio, e_maduracion, c_verdes, c_objetivo, c_inmaduras, c_sobremaduras, c_secas, pc_objetivo) VALUES ^
('2025-12-09 10:00:00', 'LOTE-001', 88.5, 15, 960, 10, 15, 5, 96.0), ^
('2025-12-10 10:00:00', 'LOTE-002', 85.2, 20, 950, 15, 20, 8, 95.0);"

echo Insertando RC Sobremaduras...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO rc_sobremaduras (tiempo, gbx, promedio, observaciones) VALUES ^
('2025-12-09 10:00:00', 15.0, 1.5, 'Normal'), ^
('2025-12-10 10:00:00', 20.0, 2.0, 'Aceptable');"

echo Insertando RC Inmaduras...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO rc_inmaduras (tiempo, gbx, promedio, observaciones) VALUES ^
('2025-12-09 10:00:00', 10.0, 1.0, 'Bajo'), ^
('2025-12-10 10:00:00', 15.0, 1.5, 'Normal');"

echo Insertando RC Maduras...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO rc_maduras (tiempo, gbx, promedio, observaciones) VALUES ^
('2025-12-09 10:00:00', 960.0, 96.0, 'Excelente'), ^
('2025-12-10 10:00:00', 950.0, 95.0, 'Muy bueno');"

echo [7/7] Insertando registros de Catación con Rondas...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO catacion (nlote, pfinales, c1agrio, c1hongos, c1negro, c2flotador, c2inmaduro) VALUES ^
('LOTE-001', 86.2, 2, 1, 0, 3, 2), ^
('LOTE-002', 84.1, 3, 2, 1, 4, 3);"

echo Insertando Rondas de Catación...
psql -h %PGHOST% -p %PGPORT% -U %PGUSER% -d %PGDATABASE% -c ^
"INSERT INTO rondas (id_catacion, valor_calidad) SELECT id_catacion, 86.0 FROM catacion WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_catacion, 86.5 FROM catacion WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_catacion, 86.0 FROM catacion WHERE nlote='LOTE-001' UNION ALL ^
 SELECT id_catacion, 84.5 FROM catacion WHERE nlote='LOTE-002' UNION ALL ^
 SELECT id_catacion, 84.8 FROM catacion WHERE nlote='LOTE-002' UNION ALL ^
 SELECT id_catacion, 83.0 FROM catacion WHERE nlote='LOTE-002';"
echo.
echo ========================================
echo   LLENADO COMPLETADO EXITOSAMENTE
echo ========================================
echo.
echo Datos insertados:
echo   - 5 Lotes en Area Acopio
echo   - 3 Procesos de Secado (con 6 Humedades, 4 Termo-Higrometrías, 5 Temperaturas, 4 Camas)
echo   - 2 Registros en Bodega (con 2 GuardarCafe)
echo   - 2 Registros de Trilla (con 2 Peso Verde)
echo   - 2 Caracterizaciones (con 2 RC de cada tipo)
echo   - 2 Cataciones (con 6 Rondas en total)
echo.
echo Puede verificar con: psql -h localhost -U postgres -d coffeebeanflow_db
echo.
pause
