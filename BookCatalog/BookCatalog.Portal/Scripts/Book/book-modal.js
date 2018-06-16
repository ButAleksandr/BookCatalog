var BookModal = BookModal || {};

(function () {
    var self = this;
    const modalSelector = '#bookModal';
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
    }

    self.Show = function (bookId) {
        $(modalSelector).modal("show");
    }

    self.initBindings = function () {
        ko.cleanNode($(modalSelector)[0]);
        ko.applyBindings(self, $(modalSelector)[0]);
    }
}).apply(BookModal);