var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};

// 詳細画面の発見ステータスチェックボックス押下時に、発見日の活性・非活性を切り替える
document.addEventListener("DOMContentLoaded", function () {
    var _a;
    (_a = document.getElementById("isFound")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", () => __awaiter(this, void 0, void 0, function* () {
        const isFound = document.getElementById("isFound").checked;
        const input_foundDate = document.getElementById("foundDate");
        // 発見済みのチェックボックスの値から、発見日の活性・非活性ステータスを切り替える
        if (isFound) {
            input_foundDate.disabled = false;
        }
        else {
            input_foundDate.disabled = true;
        }
    }));
});

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
            alert("更新が完了しました");
            // location.reload();
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
        const lostId = parseInt(document.getElementById("lostId").value);
        // サーバーに選択されたデータを送信
        const response = yield fetch("/LostApi/DeleteLost", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            // TODO: API側でint型を受け取れるようにする
            body: JSON.stringify(lostId),
        });
        if (response.ok) {
            alert("削除が完了しました");
            const baseUrl = '/'
            const url = `${baseUrl}`;
            window.location.href = url;
        }
        else {
            console.error("Failed to send data to update page");
            return;
        }
    }));    
});

// 紛失情報の入力値を取得する関数
function getLostTextValue() {

    clearErrorMessage();

    const lostItem = document.getElementById("lostItem").value;
    const lostPlace = document.getElementById("lostPlace").value;
    const lostDetailedPlace = document.getElementById("lostDetail").value;
    const lostDateValue = formatDate(document.getElementById("lostDate").value);
    const foundDateValue = formatDate(document.getElementById("foundDate").value);
    var lostDate;
    var isFound = document.getElementById("isFound").checked;
    var foundDate;
    var searchError = 0;

    if (lostDateValue) {
        if (isNaN(new Date(lostDateValue).getTime())) {
            showErrorMessage("紛失日は日付形式で入力してください。");
            searchError++;
        } else {
            lostDate = lostDateValue ? new Date(lostDateValue).toISOString() : null;
        }
    } else if (lostDateValue === "") {
        lostDate = null;
    }


    if (foundDateValue) {
        if (isNaN(new Date(foundDateValue).getTime())) {
            showErrorMessage("発見日は日付形式で入力してください。");
            searchError++;
        } else {
            foundDate = foundDateValue ? new Date(foundDateValue).toISOString() : null;
        }
    } else if (foundDateValue === "") {
        foundDate = null;
    }

    if (lostItem) {
        if (lostItem.length > 100) {
            showErrorMessage("なくしたものは100文字以内で入力してください。");
            searchError++;
        }
    }

    if (lostPlace) {
        if (lostPlace.length > 100) {
            showErrorMessage("なくした場所は100文字以内で入力してください。");
            searchError++;
        }
    }

    if (lostDetailedPlace) {
        if (lostDetailedPlace.length > 100) {
            showErrorMessage("なくした詳細な場所は100文字以内で入力してください。");
            searchError++;
        }
    }

    if (lostDate) {
        if (lostDate > new Date().toISOString()) {
            showErrorMessage("紛失日は未来の日付にできません。");
            searchError++;
        }
    }

    if (foundDate) {
        isFound = true;
        if (lostDate > new Date().toISOString()) {
            showErrorMessage("発見日は未来の日付にできません。");
            searchError++;
        }
    }

    if (searchError > 0) {
        return;
    }

    // LostIdは仮の値で1を設定（API処理の際、NULLだとエラーが発生）
    const data = {
        LostId: 1,
        UserId: 1,
        IsFound: isFound,
        LostDate: lostDate,
        FoundDate: foundDate,
        LostItem: lostItem,
        LostPlace: lostPlace,
        LostDetailedPlace: lostDetailedPlace,
        RegistrateDate: new Date().toISOString(),
        UpdateDate: new Date().toISOString()
    };
    return data;
}
