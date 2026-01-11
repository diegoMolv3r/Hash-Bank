import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RegisterFormComponent } from './modules/register/register-form.component/register-form.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RegisterFormComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('hash-bank-cliente');
}
