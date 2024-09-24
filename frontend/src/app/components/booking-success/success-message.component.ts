import { Component } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';

@Component({
  selector: 'app-success-message',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './success-message.component.html',
  styleUrl: './success-message.component.css'
})
export class SuccessMessageComponent {
  bookingId: string = '';
  constructor(private route: ActivatedRoute) {
    this.bookingId = this.route.snapshot.params['bookingId'];
  }
}
