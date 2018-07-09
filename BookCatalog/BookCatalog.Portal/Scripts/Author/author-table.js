var AuthorTable = AuthorTable || {};

(function () {
    var self = this;
    const authorTableSelector = "#authorTable";
    
    self.Initialize = function () {
        initAuthorTable();
    }

    self.Refresh = function () {
        self.table.ajax.reload();
    }

    function initAuthorTable() {
        $(authorTableSelector).dataTable().fnDestroy();

        self.table = $(authorTableSelector).DataTable({
            ajax: {
                url: self.authorsListUrl,
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
                        var authorFullName = `${row.FirstName} ${row.LastName}`;

                        return authorFullName;
                    }
                },
                {
                    targets: [2],
                    "width": "16%",
                    render: function (data, type, row) {
                        var deleteBtnElement = $("<button></button>");
                        deleteBtnElement
                            .attr({
                                "onclick": `AuthorTable.deleteAuthor('${row.Id}')`
                            })
                            .text("Delete")
                            .addClass("btn btn-danger");

                        var editBtnElement = $("<button></button>");
                        editBtnElement
                            .attr({
                                "onclick": `AuthorModal.Initialize('${row.Id}', { showAfterInit: true })`
                            })
                            .text("Edit")
                            .addClass("btn btn-primary mr-3");

                        return editBtnElement.prop("outerHTML") + deleteBtnElement.prop("outerHTML");
                    }
                }
            ]
        });
    }

    self.deleteAuthor = function (authorId) {
        jQuery.ajax({
            url: `${self.deleteAuthorUrl}?authorId=${authorId}`,
            type: "GET",
            success: function () {
                console.log("Author deleted.");

                self.Initialize();
            }
        });
    };
}).apply(AuthorTable);