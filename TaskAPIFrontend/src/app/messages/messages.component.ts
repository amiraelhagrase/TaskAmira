import { Component, OnInit } from '@angular/core';
import { MsgService } from '../send-msg/msg.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})


export class MessagesComponent implements OnInit {

  constructor(public msgService:MsgService) { }

  ngOnInit(): void {
    this.msgService.getAllMsgs();


  }

}
