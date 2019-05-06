"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/common/http");
var ServerResponseMessage = /** @class */ (function () {
    function ServerResponseMessage() {
    }
    return ServerResponseMessage;
}());
exports.ServerResponseMessage = ServerResponseMessage;
var QuickLoginCheck = /** @class */ (function () {
    function QuickLoginCheck(httpClient, cookieService) {
        this.httpClient = httpClient;
        this.cookieService = cookieService;
        this.headers = new http_1.HttpHeaders({
            'Content-Type': 'application/json',
        });
        this.IsLogin = false;
        this.options = { headers: this.headers };
    }
    QuickLoginCheck.prototype.QuickLoginCheck = function () {
        this.account = JSON.parse(this.cookieService.get("User"));
        var url = window.location.origin + '/api/Login/QuickLoginCheck';
        return this.httpClient.post(url, JSON.stringify(this.account), this.options);
    };
    return QuickLoginCheck;
}());
exports.QuickLoginCheck = QuickLoginCheck;
var ForumSelector = /** @class */ (function () {
    function ForumSelector(httpClient) {
        this.httpClient = httpClient;
    }
    return ForumSelector;
}());
exports.ForumSelector = ForumSelector;
//# sourceMappingURL=Commond.Model.js.map