import { JsonPipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { JsonStringifyPipe } from '../../pipes/json-stringify.pipe';

@Component({
  selector: 'app-response',
  imports: [JsonStringifyPipe],
  templateUrl: './response.component.html',
  styleUrl: './response.component.css',
})
export class ResponseComponent {
  @Input() response: string = '';

}
