// 初期設定
document.addEventListener("DOMContentLoaded", () => {
    // エラーメッセージ非表示
    let updateError = document.getElementById("update-front-error");
    updateError.style.display = "none";
    let deleteError = document.getElementById("delete-front-error");
    deleteError.style.display = "none";
});
// 更新ボタン押下時のエラーチェック
let updateButton = document.getElementById("update-button");
updateButton === null || updateButton === void 0 ? void 0 : updateButton.addEventListener("click", function () {
    let updateError = document.getElementById("update-front-error");
    // 初期化
    updateError.textContent = "";
    updateError.style.display = "none";
    // エラーチェック
    let errorMessage = DetailInputErrorCheck();
    if (errorMessage != "") {
        updateError.textContent = errorMessage;
        updateError.style.display = "block";
        event.preventDefault();
    }
});
// 削除ボタン押下時のエラーチェック
let deleteButton = document.getElementById("delete-button");
deleteButton === null || deleteButton === void 0 ? void 0 : deleteButton.addEventListener("click", function () {
    let deleteError = document.getElementById("delete-front-error");
    // 初期化
    deleteError.textContent = "";
    deleteError.style.display = "none";
    // エラーチェック
    let errorMessage = DetailInputErrorCheck();
    if (errorMessage != "") {
        deleteError.textContent = errorMessage;
        deleteError.style.display = "block";
        event.preventDefault();
    }
});
function DetailInputErrorCheck() {
    let errorMessaage = "";
    let lostId = document.getElementById("LostId").value;
    if (lostId == "") {
        errorMessaage += "・紛失IDが不正です。\n";
    }
    let lostDate = document.getElementById("LostDate").value;
    if (lostDate != "") {
        if (isNaN(Date.parse(lostDate))) {
            errorMessaage += "・紛失日の形式が不正です。\n";
        }
    }
    let foundDate = document.getElementById("FoundDate").value;
    if (foundDate != "") {
        if (isNaN(Date.parse(foundDate))) {
            errorMessaage += "・発見日の形式が不正です。\n";
        }
        if (lostDate != "" && !isNaN(Date.parse(lostDate))) {
            if (new Date(foundDate) < new Date(lostDate)) {
                errorMessaage += "・発見日は紛失日以降の日付を指定してください。\n";
            }
        }
    }
    let lostItem = document.getElementById("LostItem").value;
    if (lostItem != "") {
        if (lostItem.length > 100) {
            errorMessaage += "・紛失物は100文字以内で入力してください。\n";
        }
    }
    let lostPlace = document.getElementById("LostPlace").value;
    if (lostPlace != "") {
        if (lostPlace.length > 100) {
            errorMessaage += "・紛失場所は100文字以内で入力してください。\n";
        }
    }
    let lostDetailedPlace = document.getElementById("LostDetailedPlace").value;
    if (lostDetailedPlace != "") {
        if (lostDetailedPlace.length > 100) {
            errorMessaage += "・紛失した詳細な場所は100文字以内で入力してください。\n";
        }
    }
    return errorMessaage;
}
//# sourceMappingURL=detail.js.map