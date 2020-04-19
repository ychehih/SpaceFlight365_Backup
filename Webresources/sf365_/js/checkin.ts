/// <reference path="../../node_modules/@types/knockout/index.d.ts" />
namespace sf365.chekin
{
    class CheckInViewModel
    {
        isBusy: KnockoutObservable<boolean>;
        constructor() {
            this.isBusy = ko.observable(false);
        }

        public foo() {
            // some comments
            alert("foo");
        }


    }

}