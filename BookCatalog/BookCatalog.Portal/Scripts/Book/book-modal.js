var BookModal = BookModal || {};

(function () {
    var self = this;

    self.Initialize = function (bookId) {
        var container = $("#bookModalContainer");
        container.empty();

        container.load(self.loadBookModalUrl + "?bookId=" + bookId, function () {
            self.bookFormId = `#bookForm_${bookId}`;
            self.modalSelector = `#bookModal_${bookId}`;

            $.get(self.getBookBaseUrl + "?bookId=" + bookId).done(loadHandler);

            applyValidation();
            initEventHandlers();
            initDatePicker();
        });

    }

    function initVM(initData) {
        var inner = this;

        inner = ko.mapping.fromJS(initData);

        inner.AllAuthors = ko.computed(function () {
            var allAuthors = [];

            $.each(this.AllAuthors, function (index, item) {
                allAuthors.push($.extend(item, { FullName: `${item.FirstName} ${item.LastName}`}));
            });

            return allAuthors;
        }, initData);

        inner.AuthorIds = ko.computed(function () {
            var authorIds = [];

            $.each(this.Authors, function (index, item) {
                authorIds.push(item.Id);
            });

            return authorIds;
        }, initData);

        return inner;
    }

    function loadHandler(result) {
        if (result.Massage === "OK") {
            result.Value.ReleaseDate = moment(result.Value.ReleaseDate).format(Format.defaultDate.moment);

            self.vm = initVM(result.Value);

            ko.applyBindings(self.vm, $(self.bookFormId)[0]);

            refreshAuthorSelect(self.vm.AuthorIds());

            $(self.modalSelector).modal("show");
        }
    }

    function applyValidation() {
        $(self.bookFormId).validate({
            errorElement: "span",
            errorClass: "badge badge-pill badge-danger",
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
                    .closest(".form-group").addClass("has-error");
            },
            unhighlight: function (element) {
                $(element).closest(".form-group").removeClass("has-error");
            },
            success: function (label) {
                label.closest(".form-group").removeClass("has-error");
            }
        });

        $(self.bookFormId).valid();
    }

    function initEventHandlers() {
        $(".close_bookModal").on("click", function() {
            $(self.modalSelector).modal("hide");
        });

        $("#save_bookModal").on("click", saveBook);
    }

    function saveBook() {
        var mappingResult = ko.mapping.toJS(self.vm);
        mappingResult.AuthorIds = [];

        $(".selectpicker option:selected").each(function () {
            mappingResult.AuthorIds.push($(this).val());
        });

        $.post(self.saveBookBaseUrl, mappingResult)
            .done(function (data) {
                if (data.IsSuccess) {
                    $(self.modalSelector).modal("hide");

                    BookTable.Refresh();
                } else {
                    alert(`Error of savign operation: ${data.Message}`);
                }
            });
    }

    function initDatePicker() {
        $("#releaseDate").datepicker({
            format: Format.defaultDate.datePicker
        });
    }

    function refreshAuthorSelect(ids) {
        $(".selectpicker").selectpicker("val", ids);
        $(".selectpicker").selectpicker("refresh");
    }
}).apply(BookModal);