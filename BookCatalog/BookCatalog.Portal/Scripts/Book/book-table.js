var BookTable = BookTable || {};

(function () {
    var self = this;

    self.Initialize = function () {
        initBooksTable();
    }

    self.Refresh = function () {
        self.table.ajax.reload();
    }

    self.deleteBook = function (bookId) {
        jQuery.ajax({
            url: `${self.deleteBookUrl}?bookId=${bookId}`,
            type: "GET",
            success: function () {
                console.log("Book deleted succesfully.");

                self.Initialize();
            }
        });
    };

    function initBooksTable() {
        $("#bookTable").dataTable().fnDestroy();

        self.table = $("#bookTable").DataTable({
            serverSide: true, 
            ajax: {
                url: self.booksListUrl,
                type: "POST",
                dataType: "JSON" 
            },            
            columns: [
                { name: "Name", data: "Name" },
                { name: "PageCount", data: "PageCount" },
                { name: "ReleaseDate", data: "ReleaseDate" },
                { name: "Rate", data: "Rate" },
                { data: "Authors" },
                { name: "Id", data: "Id" }
            ],
            columnDefs: [
                {
                    targets: [4],
                    render: function (data, type, row) {
                        var authorLinks = [];
                        var authorsFullNames = [];

                        $.each(row.Authors, function(index, author) {
                            var authorFullName = `${author.FirstName} ${author.LastName}`;

                            var link = $(`<a href=\"#\">${authorFullName}</a>`);
                            link.attr({
                                "onclick": `AuthorModal.Initialize('${author.Id}', { showAfterInit: true })`
                            });

                            authorLinks.push(link.prop("outerHTML"));
                            authorsFullNames.push(authorFullName);
                        });

                        if (type === "sort" || type === "type") {
                            return authorsFullNames.join(", ");
                        } else {
                            return authorLinks.join(", ");
                        }
                    }
                },
                {
                    targets: [2],
                    render: function (data, type, row) {
                        return moment(row.ReleaseDate).format("DD/MM/YYYY");
                    }
                },
                {
                    targets: [5],
                    render: function (data, type, row) {
                        var deleteBtnElement = $("<button></button>");
                        deleteBtnElement
                            .attr({
                                "onclick": `BookTable.deleteBook('${row.Id}')`
                            })
                            .text("Delete")
                            .addClass("btn btn-danger");

                        var editBtnElement = $("<button></button>");
                        editBtnElement
                            .attr({
                                "onclick": `BookModal.Initialize('${row.Id}', { showAfterInit: true })`
                            })
                            .text("Edit")
                            .addClass("btn btn-primary mr-3");

                        return editBtnElement.prop("outerHTML") + deleteBtnElement.prop("outerHTML");
                    }
                }
            ]
        });
    }
}).apply(BookTable);