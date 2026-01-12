interface LostViewModel {
    /// 紛失ID
    LostId: number;
    /// ユーザーID
    UserId: number;
    /// 紛失ステータス
    IsFound: boolean;
    /// 紛失日
    LostDate: Date | null;
    /// 発見日
    FoundDate: Date | null;
    /// 紛失物
    LostItem: string | null;
    /// 紛失場所
    LostPlace: string | null;
    /// 紛失した詳細な場所
    LostDetailedPlace: string | null;
    /// 登録日（フロントでは登録日の登録をしないため、nullを許容）
    RegistrateDate: Date | null;
    /// 更新日
    UpdateDate: Date | null;
    /// メッセージ
    Message: string | null;
}

// 初期設定
// エラーメッセージ非表示
let insertError = document.getElementById("insert-front-error");
insertError.style.display = "none";

let searchError = document.getElementById("search-front-error");
searchError.style.display = "none";



// 登録ボタン押下時のエラーチェック
let insertButton = document.getElementById("insert-button");
insertButton?.addEventListener("click", function () {
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
    let errorMessaage: string = "";
    let lostDate = (<HTMLInputElement>document.getElementById("LostDate")).value;
    if (lostDate != "") {
        if (isNaN(Date.parse(lostDate))) {
            errorMessaage += "・紛失日の形式が不正です。\n";
        }
    }

    let foundDate = (<HTMLInputElement>document.getElementById("FoundDate")).value;
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

    let lostItem = (<HTMLInputElement>document.getElementById("LostItem")).value;
    if (lostItem != "") {
        if (lostItem.length > 100) {
            errorMessaage += "・紛失物は100文字以内で入力してください。\n";
        }
    }

    let lostPlace = (<HTMLInputElement>document.getElementById("LostPlace")).value;
    if (lostPlace != "") {
        if (lostPlace.length > 100) {
            errorMessaage += "・紛失場所は100文字以内で入力してください。\n";
        }
    }

    let lostDetailedPlace = (<HTMLInputElement>document.getElementById("LostDetailedPlace")).value;
    if (lostDetailedPlace != "") {
        if (lostDetailedPlace.length > 100) {
            errorMessaage += "・紛失した詳細な場所は100文字以内で入力してください。\n";
        }
    }

    return errorMessaage;
}


/// ハンズオン時のサンプルコード　削除予定
function TSButton() {
    let name: string = "Fred";
    document.getElementById("ts-example").innerHTML = greeter(user);
}

class Student {
    fullName: string;
    constructor(public firstName: string, public middleInitial: string, public lastName: string) {
        this.fullName = firstName + " " + middleInitial + " " + lastName;
    }
}

interface Person {
    firstName: string;
    lastName: string;
}

function greeter(person: Person) {
    return "Hello, " + person.firstName + " " + person.lastName;
}

let user = new Student("Fred", "M.", "Smith");