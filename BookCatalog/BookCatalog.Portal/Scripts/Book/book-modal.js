var BookModal = BookModal || {};

(function () {
    var self = this;
    const modalSelector = '#bookModal',
        bookFormId = '#bookForm';
    const Urls = {
        Edit: function (bookId) {
            return window.rootUrl + "Book/Delete?bookId=" + bookId;
        }
    }

    self.initBookModalVM = function () {
        var inner = this;

        inner.Id = ko.observable(0);
        inner.Name = ko.observable('');
        inner.PageCount = ko.observable(0);
        inner.ReleaseDate = ko.observable('');
        inner.Rate = ko.observable(0) ;
        inner.Authors = ko.observable([]);
    }

    self.Initialize = function () {
        self.initBookModalVM();
        self.initBindings();
        self.applyValidation(bookFormId);
    }

    self.applyValidation = function (formId) {
        $(formId).validate({
            errorElement: 'span',
            errorClass: 'badge badge-pill badge-danger',
            rules: {
                Name: {
                    required: true,
                    maxlength: 60
                },
                ReleaseDate: {
                    required: true
                },
                PageCount: {
                    required: true,
                    maxValue: 10000,
                    minValue: 1
                },
                Rate: {
                    required: true,
                    maxValue: 5,
                    minValue: 1
                }
            },
            errorPlacement: function (error, element) {
                var cont = $(element).parent('.form-group');
                if (cont.length > 0) {
                    cont.after(error);
                } else {
                    element.after(error);
                }
            },
            highlight: function (element) {
                $(element)
                    .closest('.form-group').addClass('has-error');
            },
            unhighlight: function (element) {
                $(element)
                    .closest('.form-group').removeClass('has-error');
            },
            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
            },
        });

        $(formId).valid();
    }

    self.Show = function (bookId) {
        $(modalSelector).modal("show");
    }

    self.initBindings = function () {
        ko.cleanNode($(modalSelector)[0]);
        ko.applyBindings(self, $(modalSelector)[0]);
    }
}).apply(BookModal);