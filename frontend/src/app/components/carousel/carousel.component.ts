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
  @Input() images: string[] | undefined = [];
  currentIndex = 0;
  next() {
    if (this.images !== undefined)
      this.currentIndex = (this.currentIndex + 1) % this.images.length;
  }
  prev() {
    if (this.images !== undefined)
      this.currentIndex = (this.currentIndex - 1 + this.images.length) % this.images.length;
  }
  goTo(index: number) {
    if (this.images !== undefined) {
      if (index >= 0 || index < this.images.length) {
        this.currentIndex = index;
      }
    }
  }
}
