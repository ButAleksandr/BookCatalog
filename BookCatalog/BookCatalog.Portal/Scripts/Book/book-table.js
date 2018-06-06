var BookTable = BookTable || {};

(function () {
    var self = this;

    self.Initialize = function () {        
        $('#bookTable').DataTable({
            ajax: {
                url: window.rootUrl + "Book/GetBooksList",
                dataSrc: ""
            },            
            type: "GET",
            columns: [
                { data: "Name" },
                { data: "PageCount" },
                { data: "ReleaseDate" },
                { data: "Rate" },
                { data: "Authors" }
            ],
            columnDefs: [
                {
                    targets: [4],
                    render: function (data, type, row) {
                        var authorLinks = [];
                        var authorsFullNames = [];

                        $.each(row.Authors, function (index, author) {
                            var authorFullName = author.FirstName + ' ' + author.LastName;

                            var link = $("<a href=\"#\">" + authorFullName + "</a>");

                            authorLinks.push(link.prop('outerHTML'));
                            authorsFullNames.push(authorFullName);
                        })

                        if (type === "sort" || type === 'type') {
                            return authorsFullNames.join(", ");
                        } else {
                            return authorLinks.join(", ");
                        }
                    }
                },
                {
                    targets: [2],
                    render: function (data, type, row) {
                        return eval(row.ReleaseDate.replace(new RegExp('/', 'g'), "")).toLocaleString('en-US');
                    }
                }
            ]
        });
    }
}).apply(BookTable);

BookTable.Initialize();