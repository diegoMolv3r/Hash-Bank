import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-atm-screen',
  imports: [],
  templateUrl: './atm-screen.html',
  styleUrl: './atm-screen.css',
})
export class AtmScreen {
  @Output() onExit = new EventEmitter<void>();

  constructor() { }

  handleLogin() {
    console.log('Navegando al login...');
  }

  handleExit() {
    this.onExit.emit();
  }
}
