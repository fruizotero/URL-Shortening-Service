import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { InputTextComponent } from "../../inputs/input-text/input-text.component";


@Component({
  selector: 'app-short-url-form',
  imports: [ReactiveFormsModule, InputTextComponent],
  templateUrl: './short-url-form.component.html',
  styleUrl: './short-url-form.component.css'
})
export class ShortUrlFormComponent {

  form = new FormGroup({
    method: new FormControl("get"),
    shortUrl: new FormControl(""),
  });

  // vincular mi formulario con el componente

  onSubmit() {
    console.log("Formulario enviado", this.form.value);
   }



}
