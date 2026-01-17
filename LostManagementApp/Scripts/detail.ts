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
document.addEventListener("DOMContentLoaded", () => {
    // エラーメッセージ非表示
    let updateError = document.getElementById("update-front-error");
    updateError.style.display = "none";

    let deleteError = document.getElementById("delete-front-error");
    deleteError.style.display = "none";
});

// 更新ボタン押下時のエラーチェック
let updateButton = document.getElementById("update-button");
updateButton?.addEventListener("click", function () {
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
deleteButton?.addEventListener("click", function () {
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
    let errorMessaage: string = "";

    let lostId = (<HTMLInputElement>document.getElementById("LostId")).value;
    if (lostId == "") {
        errorMessaage += "・紛失IDが不正です。\n";
    }

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