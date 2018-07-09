var AuthorModal = AuthorModal || {};

(function () {
    var self = this;

    self.Initialize = function (authorId) {
        if (authorId != null && authorId >= 0) {
            var container = $("#authorModalContainer");
            container.empty();

            container.load(self.getAuthorModalUrl + "?authorId=" + authorId, function () {
                self.formId = `#authorForm_${authorId}`;
                self.modalSelector = `#authorModal_${authorId}`;

                loadAuthor(authorId);
            });
        }
    }
    
    function loadAuthor(authorId) {
        $.get(`${self.getAuthorBaseUrl}?authorId=${authorId}`)
            .done(onLoad);
    }

    function saveAuthor() {
        $.post(`${self.saveAuthorBaseUrl}`, ko.mapping.toJS(self.vm))
            .done(onSave);
    }

    function onLoad(result) {
        if (result.Massage === "OK") {
            self.vm = ko.mapping.fromJS(result.Value);;

            applyValidation();

            initEventHandlers();

            ko.applyBindings(self.vm, $(self.formId)[0]);

            $(self.modalSelector).modal("show");
        }
    }

    function onSave(data) {
        if (data.IsSuccess) {
            $(self.modalSelector).modal("hide");

            self.targetTable.Refresh();
        } else {
            alert(`Error of savign operation. ${data.Message}`);
        };
    }

    function applyValidation() {
        $(self.formId).validate({
            errorElement: 'span',
            errorClass: 'badge badge-pill badge-danger',
            rules: {
                FirstName: {
                    required: true,
                    maxlength: 60
                },
                LastNames: {
                    required: true,
                    maxlength: 60
                }
            },
            errorPlacement: function (error, element) {
                element.after(error);
            },
            highlight: function (element) {
                $(element)
                    .closest('.form-group').addClass('has-error');
            },
            unhighlight: function (element) {
                $(element).closest('.form-group').removeClass('has-error');
            },
            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
            }
        });

        $(self.formId).valid();
    }

    function initEventHandlers() {
        $(".close_authorModal").on("click", function() {
            $(self.modalSelector).modal("hide");
        });

        $("#save_authorModal").on("click", saveAuthor);
    }
}).apply(AuthorModal);