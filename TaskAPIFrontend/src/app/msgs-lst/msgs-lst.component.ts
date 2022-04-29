import { Component, OnInit } from '@angular/core';
import { MsgService } from '../send-msg/msg.service';
@Component({
  selector: 'app-msgs-lst',
  templateUrl: './msgs-lst.component.html',
  styleUrls: ['./msgs-lst.component.css']
})
export class MsgsLstComponent implements OnInit {

  constructor(public msgService:MsgService) { }

  ngOnInit(): void {
    this.msgService.getAllMsgs();
  }

}
