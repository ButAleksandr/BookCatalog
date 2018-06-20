var BookModal = BookModal || {};

(function () {
    var self = this,
        initStatus = {
            Validation: false,
            Bindings: false
        },
        bookEditBaseUrl = '';

    const modalSelector = '#bookModal',
        bookFormId = '#bookForm',
        Urls = {
            EditBook: function (bookId) {
                return bookEditBaseUrl + "?bookId=" + bookId;
            }
        }

    self.VM = function () {
        var inner = this;

        VMObject = {
            Id: inner.Id(),
            Name: inner.Name(),
            PageCount: inner.PageCount(),
            ReleaseDate: inner.ReleaseDate(),
            Rate: inner.Rate(),
            Authors: inner.Authors()
        };

        return VMObject;
    }

    self.initVM = function (initObject, bookModalSettings) {
        var inner = this;

        if (!initObject) {
            initObject = {
                Id: 0,
                Name: '',
                PageCount: '',
                ReleaseDate: '',
                Rate: 0,
                Authors: []
            };
        }

        inner.Id = ko.observable(initObject.Id);
        inner.Name = ko.observable(initObject.Name);
        inner.PageCount = ko.observable(initObject.PageCount);
        inner.ReleaseDate = ko.observable(initObject.ReleaseDate);
        inner.Rate = ko.observable(initObject.Rate);
        inner.Authors = ko.observable(initObject.Authors);

        if (bookModalSettings
            && bookModalSettings.hasOwnProperty('showAfterInit')
            && bookModalSettings.showAfterInit) {
            self.show();            
        };
    }

    self.Initialize = function (bookId, bookModalSettings) {
        self.initVariables(bookModalSettings);

        if (!bookId) {
            self.initVM(null, bookModalSettings);
        } else {
            self.loadBook(bookId, bookModalSettings);
        }        

        if (!initStatus.Bindings) {
            self.initBindings();
        }

        if (!initStatus.Validation) {
            self.applyValidation(bookFormId);
        }
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
                    max: 10000,
                    min: 1
                },
                Rate: {
                    required: true,
                    max: 5,
                    min: 1
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

        $(formId).valid();

        initStatus.Validation = true;
    }

    self.show = function () {
        $(modalSelector).modal("show");
    }

    self.loadBook = function (bookId, bookModalSettings) {
        $.get(Urls.EditBook(bookId))
            .done(function (result) {
                if (result.Massage = "OK") {
                    self.initVM(result.Value, bookModalSettings);
                }
            });
    }

    self.initVariables = function (bookModalSettings) {
        if (bookModalSettings && bookModalSettings.hasOwnProperty('getBookUrl')) {
            bookEditBaseUrl = bookModalSettings.getBookUrl;
        }
    }

    self.initBindings = function () {
        //ko.cleanNode($(modalSelector)[0]);
        ko.applyBindings(self, $(bookFormId)[0]);

        initStatus.Bindings = true;
    }
}).apply(BookModal);