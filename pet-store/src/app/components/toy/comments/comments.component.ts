import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
// import { Comment } from 'src/app/models/toy/comments/comment';
import { PetStoreService } from 'src/app/services/pet-store.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})

export class CommentsComponent implements OnInit {

  comment: Comment;
  commentsForm: FormGroup;

  constructor(private api: PetStoreService) {
    this.commentsForm = new FormGroup({

    });
    this.comment = new Comment();
  }

  ngOnInit() {
  }

  submitComment() {
    this.api.createComment(this.comment).subscribe();
  }

}
