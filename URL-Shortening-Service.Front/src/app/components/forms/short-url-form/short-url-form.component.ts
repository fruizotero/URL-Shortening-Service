import { Component, EventEmitter, Output, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { InputTextComponent } from '../../inputs/input-text/input-text.component';
import { FormShortUrl } from '../../../interfaces/form-short-url';

@Component({
  selector: 'app-short-url-form',
  imports: [ReactiveFormsModule, InputTextComponent],
  templateUrl: './short-url-form.component.html',
  styleUrl: './short-url-form.component.css',
})
export class ShortUrlFormComponent {
  @Output() formSubmitted = new EventEmitter<FormShortUrl>();
  label: string = 'Short URL';
  placeholder: string = 'Enter a URL';
  isUpdate = signal(false);

  form = new FormGroup({
    method: new FormControl('get'),
    shortUrl: new FormControl(''),
    newUrl: new FormControl(''),
  });

  // vincular mi formulario con el componente

  onSubmit() {
    this.formSubmitted.emit(this.form.value as FormShortUrl);
  }

  onchangeMethod(event: Event) {

    let element = event.target as HTMLSelectElement;
    let value = element.value;
    this.isUpdate.set(false)

    switch (value) {
      case 'get':
        this.label = 'Short URL';
        break;

      case 'post':
        this.label = 'Add URL';
        break;
      case 'put':
        this.label = 'Update URL';
        this.isUpdate.set(true);
        break;
      case 'delete':
        this.label = 'Delete URL';
        break;

      default:
        break;
     }

  }
}
