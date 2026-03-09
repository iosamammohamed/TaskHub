import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToastModule } from 'primeng/toast';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { AuthService } from './core/auth/auth.service';
import { NavbarComponent } from './core/components/navbar/navbar.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, ToastModule, ButtonModule, NavbarComponent],

  templateUrl: './app.component.html',
  styles: []
})
export class AppComponent {
  title = 'taskhub-web';

  constructor(public authService: AuthService) { }

}


