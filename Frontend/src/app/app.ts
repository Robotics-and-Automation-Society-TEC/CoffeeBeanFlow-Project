import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ChatbotComponent } from './shared/components/chatbot/chatbot';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ChatbotComponent],
  template: `
    <router-outlet></router-outlet>
    <app-chatbot></app-chatbot>
  `,
  styleUrl: './app.css'
})
export class App {
  title = 'CoffeeBeanFlow';
}
