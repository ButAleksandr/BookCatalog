var AuthorTable = AuthorTable || {};

(function () {
    var self = this;
    const authorsListUrl = window.rootUrl + "Author/AuthorsList";
    const authorTableSelector = '#authorTable';
    const Urls = {
        DeleteAuthor: function (authorId) {
            return window.rootUrl + "Author/Delete?authorId=" + authorId;
        }
    }
    self.Initialize = function () {
        self.initAuthorTable(self.initBindings);
    }

    self.initBindings = function () {
        ko.cleanNode($(authorTableSelector)[0]);
        ko.applyBindings(self, $(authorTableSelector)[0]);
    }

    self.Refresh = function () {
        self.table.ajax.reload();
    }

    self.initAuthorTable = function (postInitAction) {
        $(authorTableSelector).dataTable().fnDestroy();

        self.table = $(authorTableSelector).DataTable({
            ajax: {
                url: authorsListUrl,
                dataSrc: "Value"
            },
            type: "GET",
            columns: [
                { data: "FirstName" },
                { data: "BookCount"}
            ],
            columnDefs: [                
                {
                    targets: [0],
                    render: function (data, type, row) {
                        var authorFullName = row.FirstName + ' ' + row.LastName;

                        return authorFullName;
                    }
                },
                {
                    targets: [1],
                    "width": "16%",
                    render: function (data, type, row) {
                        var deleteBtnElement = $("<button></button>");
                        deleteBtnElement
                            .attr({
                                "onclick": "AuthorTable.deleteAuthor('" + row.Id + "')"
                            })
                            .text("Delete")
                            .addClass("btn btn-danger");

                        var editBtnElement = $("<button></button>");
                        editBtnElement
                            .text("Edit")
                            .addClass("btn btn-primary mr-3");

                        return editBtnElement.prop('outerHTML') + deleteBtnElement.prop('outerHTML');
                    }
                }
            ],
            initComplete: postInitAction
        });
    }

    self.deleteAuthor = function (authorId) {
        jQuery.ajax({
            url: Urls.DeleteAuthor(authorId),
            type: "GET",
            success: function () {
                console.log("Author deleted.");

                self.Initialize();
            }
        });
    };
}).apply(AuthorTable);