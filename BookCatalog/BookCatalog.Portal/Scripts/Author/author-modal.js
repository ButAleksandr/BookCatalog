var AuthorModal = AuthorModal || {};

(function () {
    var self = this,
        initStatus = {
            Validation: false,
            Bindings: false,
            EventHandlers: false,
            DatePicker: false
        },
        getAuthorBaseUrl = '',
        saveAuthorBaseUrl = '';

    const modalSelector = '#authorModal',
        authorFormId = '#authorForm',
        Urls = {
            getAuthor: function (authorId) {
                return getAuthorBaseUrl + "?authorId=" + authorId;
            },
            saveAuthor: function () {
                return saveAuthorBaseUrl;
            }
        };

    self.onCreate = function () {
        self.Initialize(0, { showAfterInit: true });
    }

    self.JsObject = function () {
        var vm = self.VM;
        var authorIds = [];

        $('.selectpicker option:selected').each(function () {
            authorIds.push($(this).val());
        })

        JsObject = {
            Id: vm.Id(),
            FirstName: vm.FirstName(),
            LastName: vm.LastName()
        };

        return JsObject;
    }

    function InitVM(initObject, authorModalSettings) {
        var inner = this;

        if (!initObject) {
            initObject = {
                Id: 0,
                FirstName: '',
                LastName: ''
            };
        }       

        inner.Id = ko.observable(initObject.Id);
        inner.FirstName = ko.observable(initObject.FirstName);
        inner.LastName = ko.observable(initObject.LastName);

        if (authorModalSettings
            && authorModalSettings.hasOwnProperty('showAfterInit')
            && authorModalSettings.showAfterInit) {
            self.show();
        };
    }

    self.refreshVM = function (vm, initObject) {
        vm.Id(initObject.Id);
        vm.FirstName(initObject.FirstName);
        vm.LastName(initObject.LastName);
    }

    self.Initialize = function (authorId, authorModalSettings) {
        self.initVariables(authorModalSettings);

        if (!initStatus.Bindings) {
            self.VM = new InitVM(null, authorModalSettings);
            self.initBindings(self.VM);
        }        

        if (!initStatus.Validation) {
            self.applyValidation(authorFormId);
        }

        if (!initStatus.EventHandlers) {
            self.initEventHandlers();
        }

        if (authorId != null && authorId >= 0) {
            self.loadAuthor(authorId, authorModalSettings);
        }
    }

    self.applyValidation = function (formId) {
        $(formId).validate({
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

        $(formId).valid();

        initStatus.Validation = true;
    }

    self.show = function () {
        $(modalSelector).modal("show");
    }

    self.clean = function () {
        self.VM.Id(0);
        self.VM.FirstName('');
        self.VM.LastName('');
    }

    self.loadAuthor = function (authorId, authorModalSettings) {
        $.get(Urls.getAuthor(authorId))
            .done(function (result) {
                if (result.Massage = "OK") {                    
                    self.refreshVM(self.VM, result.Value);                    
                    self.show();                   
                }
            });
    }

    self.saveAuthor = function () {
        $.post(Urls.saveAuthor(), self.JsObject())
            .done(function (data) {
                if (data.IsSuccess) {
                    $(modalSelector).modal("hide");
                    AuthorTable.Refresh();
                } else {
                    alert("Error of savign operation. " + data.Message);
                }     
            });
    }

    self.initVariables = function (authorModalSettings) {
        if (authorModalSettings && authorModalSettings.hasOwnProperty('getAuthorUrl')) {
            getAuthorBaseUrl = authorModalSettings.getAuthorUrl;
            saveAuthorBaseUrl = authorModalSettings.saveAuthorUrl;
        }
    }

    self.initBindings = function (vm) {
        ko.applyBindings(vm, $(authorFormId)[0]);     
        ko.applyBindings(vm, $("#createBlock")[0]);      

        initStatus.Bindings = true;
    }

    self.hide = function () {
        $(modalSelector).modal("hide");
    }

    self.initEventHandlers = function () {
        $(".close_authorModal").on("click", self.hide);
        $("#save_authorModal").on("click", self.saveAuthor);

        initStatus.EventHandlers = true;
    }
}).apply(AuthorModal);