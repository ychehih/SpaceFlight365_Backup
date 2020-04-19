/// <reference path="../../node_modules/@types/knockout/index.d.ts" />
var sf365;
(function (sf365) {
    var chekin;
    (function (chekin) {
        var CheckInViewModel = /** @class */ (function () {
            function CheckInViewModel() {
                this.isBusy = ko.observable(false);
            }
            CheckInViewModel.prototype.foo = function () {
                alert("foo");
            };
            return CheckInViewModel;
        }());
    })(chekin = sf365.chekin || (sf365.chekin = {}));
})(sf365 || (sf365 = {}));
//# sourceMappingURL=checkin.js.map