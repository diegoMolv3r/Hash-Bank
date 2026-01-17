import { Component, EventEmitter, Output} from '@angular/core';
import { ViewWelcome } from "../atm-views/view-welcome/view-welcome";
import { ViewMenu } from "../atm-views/view-menu/view-menu";
export type ScreenViewMode = 'WELCOME' | 'TRANSACTION';

@Component({
  selector: 'app-atm-screen',
  imports: [ViewWelcome, ViewMenu],
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
