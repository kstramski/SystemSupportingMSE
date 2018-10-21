import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-modal-delete',
  template: `
    <div class="modal fade" id="modalId" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="modalLabel">{{modalTitle}}</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        {{modalBody}}
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-danger" (click)="delete()" data-dismiss="modal">Delete</button>
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>`
})

export class ModalDeleteComponent {
  @Input('modal-title') modalTitle;
  @Input('modal-body') modalBody;
  @Output('delete-clicked') deleteClicked = new EventEmitter();

  delete() {
    this.deleteClicked.emit();
  }
}