var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};

// 更新ボタン押下時の処理
document.addEventListener("DOMContentLoaded", function () {
    var _a;
    (_a = document.getElementById("update-button")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", () => __awaiter(this, void 0, void 0, function* () {
        // テキストボックスのデータを取得
        var data = getLostTextValue();


        // サーバーに選択されたデータを送信
        const response = yield fetch("/LostApi/UpdateLost", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        if (response.ok) {
            // 詳細ページをリロードする
            location.reload();
        }
        else {
            console.error("Failed to send data to update page");
            return;
        }
    }));
});


// 削除ボタン押下時の処理
document.addEventListener("DOMContentLoaded", function () {
    var _a;
    (_a = document.getElementById("delete-button")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", () => __awaiter(this, void 0, void 0, function* () {
        const selectedItems = getCheckLostId();
        if (selectedItems.length === 0) {
            alert("更新するデータを選択してください。");
            return;
        }
        // サーバーに選択されたデータを送信
        const response = yield fetch("/LostApi/DeleteLost", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(selectedItems),
        });
        if (response.ok) {
            location.reload();
        }
        else {
            console.error("Failed to send data to update page");
            return;
        }
    }));
});


