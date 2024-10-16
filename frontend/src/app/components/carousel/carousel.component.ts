import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-carousel',
  standalone: true,
  imports: [CommonModule,],
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.css'
})
export class CarouselComponent {
  @Input() images: string[] = [];
  currentIndex = 0;
  next() {
    this.currentIndex = (this.currentIndex + 1) % this.images.length;
  }
  prev() {
    this.currentIndex = (this.currentIndex - 1 + this.images.length) % this.images.length;
  }

  goTo(index: number) {
    if (index >= 0 || index < this.images.length) {
      this.currentIndex = index;
    }
  }
}
