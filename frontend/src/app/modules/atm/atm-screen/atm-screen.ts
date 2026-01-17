import { Component, EventEmitter, Output} from '@angular/core';
  
export type ScreenViewMode = 'WELCOME' | 'TRANSACTION';

@Component({
  selector: 'app-atm-screen',
  imports: [],
  templateUrl: './atm-screen.html',
  styleUrl: './atm-screen.css',
})
export class AtmScreen {
  @Output() onEnterScreen = new EventEmitter<void>();
  @Output() onExit = new EventEmitter<void>();

  viewMode: ScreenViewMode = 'WELCOME';

  constructor() { }

  handleScreenTouch() {
    this.viewMode = 'TRANSACTION'; 
    this.onEnterScreen.emit();     
  }

  handleExit() {
    this.viewMode = 'WELCOME'; 
    this.onExit.emit();        
  }
}
