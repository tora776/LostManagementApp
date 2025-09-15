// 日付を yyyy/MM/dd 形式にフォーマットする関数
function formatDate(dateString) {
    if (!dateString) return "";
    const date = new Date(dateString);
    return `${date.getFullYear()}/${(date.getMonth() + 1).toString().padStart(2, "0")}/${date.getDate().toString().padStart(2, "0")}`;
}

function getLostTextValue() {
    const lostId = document.getElementById("lostId").value;
    const lostItem = document.getElementById("lostItem").value;
    const lostPlace = document.getElementById("lostPlace").value;
    const isFound = document.getElementById("isFound").checked;
    const lostDetailedPlace = document.getElementById("lostDetail").value;
    const lostDateValue = formatDate(document.getElementById("lostDate").value);
    const foundDateValue = formatDate(document.getElementById("foundDate").value);
    const updateDateValue = formatDate(new Date().toString());

    // ISO8601形式に変換（空の場合はnull）
    const lostDate = lostDateValue ? new Date(lostDateValue).toISOString() : null;
    const foundDate = foundDateValue ? new Date(foundDateValue).toISOString() : null;
    const updateDate = updateDateValue ? new Date(updateDateValue).toISOString() : null;

    if (!lostItem || !lostPlace || !lostDetailedPlace) {
        alert("全ての項目を入力してください。");
        return;
    }

    // 日本時間でyyyy/MM/dd形式の文字列を生成
    //const jpDate = new Date();
    //const jpTime = jpDate.toLocaleString("ja-JP", { timeZone: "Asia/Tokyo" });
    //const dateObj = new Date(jpTime);
    //const updateDate = `${dateObj.getFullYear()}/${(dateObj.getMonth() + 1).toString().padStart(2, "0")}/${dateObj.getDate().toString().padStart(2, "0")}`;

    const data = {
        LostId: parseInt(lostId),
        UserId: 1,
        IsFound: isFound,
        LostDate: lostDate,
        FoundDate: foundDate,
        LostItem: lostItem,
        LostPlace: lostPlace,
        LostDetailedPlace: lostDetailedPlace,
        RegistrateDate: null,
        UpdateDate: updateDate
    };

    return data;
}