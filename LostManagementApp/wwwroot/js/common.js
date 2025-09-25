// 日付を yyyy/MM/dd 形式にフォーマットする関数
function formatDate(dateString) {
    if (!dateString) return "";
    const date = new Date(dateString);
    return `${date.getFullYear()}/${(date.getMonth() + 1).toString().padStart(2, "0")}/${date.getDate().toString().padStart(2, "0")}`;
}

// エラーメッセージを表示する関数
function showErrorMessage(message) {
    const errorDiv = document.getElementById("search-error-message");
    if (errorDiv) {
        // 既存のメッセージがあれば<br>で区切る
        if (errorDiv.innerHTML !== "") {
            errorDiv.innerHTML += "<br>";
        }
        errorDiv.innerHTML += message;
        errorDiv.style.display = "block";
    }
}

// エラーメッセージを初期化する関数
function clearErrorMessage() {
    const errorDiv = document.getElementById("search-error-message");
    if (errorDiv) {
        errorDiv.textContent = "";
        errorDiv.style.display = "none";
    }
}