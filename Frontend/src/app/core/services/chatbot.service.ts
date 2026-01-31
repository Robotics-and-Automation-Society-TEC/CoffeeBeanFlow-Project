import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ChatResponse {
  success: boolean;
  response?: string;
  error?: string;
}

@Injectable({
  providedIn: 'root'
})
export class ChatbotService {
  private apiUrl = 'http://192.168.0.47:5253/api/Chatbot';

  constructor(private http: HttpClient) {}

  ask(message: string): Observable<ChatResponse> {
    return this.http.post<ChatResponse>(`${this.apiUrl}/ask`, { message });
  }
}
