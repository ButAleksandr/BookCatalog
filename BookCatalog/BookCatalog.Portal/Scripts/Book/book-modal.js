var BookModal = BookModal || {};

(function () {
    var self = this,
        initStatus = {
            Validation: false,
            Bindings: false,
            EventHandlers: false
        },
        getBookBaseUrl = '',
        saveBookBaseUrl = '';

    const modalSelector = '#bookModal',
        bookFormId = '#bookForm',
        Urls = {
            getBook: function (bookId) {
                return getBookBaseUrl + "?bookId=" + bookId;
            },
            saveBook: function () {
                return saveBookBaseUrl;
            }
        }

    self.JsObject = function () {
        var vm = self.VM;

        JsObject = {
            Id: vm.Id(),
            Name: vm.Name(),
            PageCount: vm.PageCount(),
            ReleaseDate: vm.ReleaseDate(),
            Rate: vm.Rate(),
            Authors: vm.Authors()
        };

        return JsObject;
    }

    function InitVM(initObject, bookModalSettings) {
        var inner = this;

        if (!initObject) {
            initObject = {
                Id: 0,
                Name: '',
                PageCount: '',
                ReleaseDate: moment(new Date()).format("MM/DD/YYYY"),
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

    self.refreshVM = function (vm, initObject) {
        vm.Id(initObject.Id);
        vm.Name(initObject.Name);
        vm.PageCount(initObject.PageCount);
        vm.ReleaseDate(initObject.ReleaseDate);
        vm.Rate(initObject.Rate);
        vm.Authors(initObject.Authors);
    }

    self.Initialize = function (bookId, bookModalSettings) {
        self.initVariables(bookModalSettings);

        if (!initStatus.Bindings) {
            self.VM = new InitVM(null, bookModalSettings);
            self.initBindings(self.VM);
        }        

        if (!initStatus.Validation) {
            self.applyValidation(bookFormId);
        }

        if (!initStatus.EventHandlers) {
            self.initEventHandlers();
        }

        if (bookId > 0) {
            self.loadBook(bookId, bookModalSettings);
        } 

        if (bookId == 0) {
            self.clean();
            self.show();
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

    self.clean = function () {
        self.VM.Id(0);
        self.VM.Name('');
        self.VM.PageCount(0);
        self.VM.ReleaseDate(moment(new Date()).format("MM/DD/YYYY"));
        self.VM.Rate(0);
        self.VM.Authors([]);
    }

    self.loadBook = function (bookId, bookModalSettings) {
        $.get(Urls.getBook(bookId))
            .done(function (result) {
                if (result.Massage = "OK") {
                    result.Value.ReleaseDate = moment(result.Value.ReleaseDate).format("MM/DD/YYYY");
                    self.refreshVM(self.VM, result.Value);
                    self.show();                   
                }
            });
    }

    self.saveBook = function () {
        $.post(Urls.saveBook(), self.JsObject())
            .done(function (data) {
                if (data.IsSuccess) {
                    $(modalSelector).modal("hide");
                    BookTable.Refresh();
                } else {
                    alert("Error of savign operation./n" + data.Message);
                }     
            });
    }

    self.initVariables = function (bookModalSettings) {
        if (bookModalSettings && bookModalSettings.hasOwnProperty('getBookUrl')) {
            getBookBaseUrl = bookModalSettings.getBookUrl;
            saveBookBaseUrl = bookModalSettings.saveBookUrl;
        }
    }

    self.initBindings = function (vm) {
        ko.applyBindings(vm, $(bookFormId)[0]);

        initStatus.Bindings = true;
    }

    self.hide = function () {
        $(modalSelector).modal("hide");
    }

    self.initEventHandlers = function () {
        $(".close_bookModal").on("click", self.hide);
        $("#save_bookModal").on("click", self.saveBook);
        initStatus.EventHandlers = true;
    }
}).apply(BookModal);