import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { InputTextComponent } from "../../inputs/input-text/input-text.component";
import { FormShortUrl } from '../../../interfaces/form-short-url';


@Component({
  selector: 'app-short-url-form',
  imports: [ReactiveFormsModule, InputTextComponent],
  templateUrl: './short-url-form.component.html',
  styleUrl: './short-url-form.component.css'
})
export class ShortUrlFormComponent {

  @Output() formSubmitted = new EventEmitter<FormShortUrl>();

  form = new FormGroup({
    method: new FormControl("get"),
    shortUrl: new FormControl(""),
  });

  // vincular mi formulario con el componente

  onSubmit() {
    this.formSubmitted.emit(this.form.value as FormShortUrl);
   }



}
