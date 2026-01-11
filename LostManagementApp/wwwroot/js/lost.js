// 初期設定
// エラーメッセージ非表示
let insertError = document.getElementById("insert-front-error");
insertError.style.display = "none";
let searchError = document.getElementById("search-front-error");
searchError.style.display = "none";
// 登録ボタン押下時のエラーチェック
let insertButton = document.getElementById("insert-button");
insertButton === null || insertButton === void 0 ? void 0 : insertButton.addEventListener("click", function () {
    let insertError = document.getElementById("insert-front-error");
    // 初期化
    insertError.textContent = "";
    insertError.style.display = "none";
    // エラーチェック
    let errorMessage = InsertErrorCheck();
    if (errorMessage != "") {
        insertError.textContent = errorMessage;
        insertError.style.display = "block";
        event.preventDefault();
    }
});
function InsertErrorCheck() {
    let errorMessaage = "";
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
/// ハンズオン時のサンプルコード　削除予定
function TSButton() {
    let name = "Fred";
    document.getElementById("ts-example").innerHTML = greeter(user);
}
class Student {
    constructor(firstName, middleInitial, lastName) {
        this.firstName = firstName;
        this.middleInitial = middleInitial;
        this.lastName = lastName;
        this.fullName = firstName + " " + middleInitial + " " + lastName;
    }
}
function greeter(person) {
    return "Hello, " + person.firstName + " " + person.lastName;
}
let user = new Student("Fred", "M.", "Smith");
//# sourceMappingURL=lost.js.map