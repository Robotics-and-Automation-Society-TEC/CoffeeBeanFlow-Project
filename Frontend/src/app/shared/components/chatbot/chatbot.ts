import { ChangeDetectorRef, Component, NgZone } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { trigger, transition, style, animate } from '@angular/animations';
import { ChatbotService } from '../../../core/services/chatbot.service';

@Component({
  selector: 'app-chatbot',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chatbot.html',
  styleUrls: ['./chatbot.css'],
  animations: [
    trigger('slideIn', [
      transition(':enter', [
        style({ transform: 'translateY(20px) scale(0.95)', opacity: 0 }),
        animate('300ms cubic-bezier(0.34, 1.56, 0.64, 1)', style({ transform: 'translateY(0) scale(1)', opacity: 1 }))
      ]),
      transition(':leave', [
        animate('200ms ease-out', style({ transform: 'translateY(20px) scale(0.95)', opacity: 0 }))
      ])
    ]),
    trigger('fadeIn', [
      transition(':enter', [
        style({ transform: 'translateY(10px)', opacity: 0 }),
        animate('250ms ease-out', style({ transform: 'translateY(0)', opacity: 1 }))
      ])
    ])
  ]
})
export class ChatbotComponent {
  isOpen = false;
  isLoading = false;
  currentMessage = '';
  messages: { text: string; isUser: boolean; time: string }[] = [];

  constructor(
    private chatbotService: ChatbotService,
    private zone: NgZone,
    private cdr: ChangeDetectorRef
  ) {}

  toggle(): void {
    this.isOpen = !this.isOpen;
  }

  clear(): void {
    this.messages = [];
    this.currentMessage = '';
  }

  send(): void {
    const text = this.currentMessage.trim();
    if (!text || this.isLoading) return;

    if (!this.isOpen) this.isOpen = true;
    this.messages.push({ text, isUser: true, time: this.getTime() });
    this.currentMessage = '';
    this.isLoading = true;
    this.scrollToBottom();

    this.chatbotService.ask(text).subscribe({
      next: (res) => {
        this.zone.run(() => {
          this.isLoading = false;
          if (res.success && res.response) {
            this.messages.push({ text: res.response, isUser: false, time: this.getTime() });
          } else {
            this.messages.push({ text: res.error || 'Error desconocido', isUser: false, time: this.getTime() });
          }
          this.scrollToBottom();
          this.cdr.detectChanges();
        });
      },
      error: () => {
        this.zone.run(() => {
          this.isLoading = false;
          this.messages.push({ text: 'Error al consultar el chatbot', isUser: false, time: this.getTime() });
          this.scrollToBottom();
          this.cdr.detectChanges();
        });
      }
    });
  }

  formatMessage(text: string): string {
    // Escapar HTML pero preservar saltos de línea
    const escaped = text
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;')
      .replace(/'/g, '&#039;');
    
    // Convertir saltos de línea a <br>
    return escaped.replace(/\n/g, '<br>');
  }

  private getTime(): string {
    return new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
  }

  private scrollToBottom(): void {
    setTimeout(() => {
      const el = document.querySelector('.chat-messages');
      if (el) {
        el.scrollTop = el.scrollHeight;
      }
    }, 50);
  }
}
