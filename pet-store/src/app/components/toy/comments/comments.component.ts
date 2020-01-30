import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CommentData } from 'src/app/models/toy/comments/commentData';
import { PetStoreService } from 'src/app/services/pet-store.service';
import { CommentsUnit } from 'src/app/models/toy/comments/commentsUnit';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})

export class CommentsComponent implements OnInit {

  comments: CommentsUnit[];
  commentData: CommentData;
  commentsForm: FormGroup;

  constructor(private api: PetStoreService, private route: ActivatedRoute) {

    this.commentsForm = new FormGroup({
      text: new FormControl(Validators.required),
      author: new FormControl(Validators.required)
    });

    this.commentData = new CommentData();
    this.route.paramMap.subscribe(
      params => {
        this.commentData.toyId = +params.get('id');
        this.getComments();
      }
    );

  }

  ngOnInit() {
  }

  getComments() {
    this.api.getComments(this.commentData.toyId).subscribe(res => {
      this.comments = res;
    });
  }

  submitComment() {
    this.api.createComment(this.commentData).subscribe(res => this.getComments());
  }

}
