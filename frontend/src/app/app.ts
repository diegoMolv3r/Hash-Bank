import { Component, signal } from '@angular/core';
import { RegisterFormComponent } from './modules/register/register-form.component/register-form.component';
import { AtmSceneComponent } from "./modules/atm-scene/atm-scene";

@Component({
  selector: 'app-root',
  imports: [ RegisterFormComponent, AtmSceneComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('hash-bank-cliente');
}
