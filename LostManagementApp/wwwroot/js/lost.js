var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
// 検索ボタン押下時の処理
document.addEventListener("DOMContentLoaded", function () {
    var _a;
    (_a = document.getElementById("search-button")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", () => __awaiter(this, void 0, void 0, function* () {
        const lostDate = new Date(document.getElementById("lostDate").value);
        const foundDate = new Date(document.getElementById("foundDate").value);
        const lostItem = document.getElementById("lostItem").value;
        const lostPlace = document.getElementById("lostPlace").value;
        const lostDetailedPlace = document.getElementById("lostDetail").value;
        const data = {
            UserId: 1,
            IsFound: false,
            LostDate: lostDate,
            FoundDate: foundDate,
            LostItem: lostItem,
            LostPlace: lostPlace,
            LostDetailedPlace: lostDetailedPlace,
        };
        const response = yield fetch("/LostApi/GetLost", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
        if (!response.ok) {
            console.error("Failed to fetch data");
            return;
        }
        // 取得したデータをJSON形式でパース
        const results = yield response.json();
        // テーブルを初期化
        initTable();
        const tableBody = document.querySelector("table tbody");
        // 検索結果が0件の場合、メッセージを表示
        if (results.length === 0) {
            const message = document.createElement("p");
            message.textContent = "検索結果がありません";
            message.className = "text-center text-danger";
            tableBody === null || tableBody === void 0 ? void 0 : tableBody.appendChild(message);
            return;
        }
        else {
            createTable(results);
        }
    }));
});
// 追加ボタン押下時の処理
document.addEventListener("DOMContentLoaded", function () {
    var _a;
    (_a = document.getElementById("insert-button")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", () => __awaiter(this, void 0, void 0, function* () {
        const lostDate = new Date(document.getElementById("lostDate").value);
        const foundDate = new Date(document.getElementById("foundDate").value);
        const lostItem = document.getElementById("lostItem").value;
        const lostPlace = document.getElementById("lostPlace").value;
        const lostDetailedPlace = document.getElementById("lostDetail").value;
        // lostItem, lostPlace, lostDetailedPlaceが空でないことを確認
        if (!lostItem || !lostPlace || !lostDetailedPlace) {
            alert("全ての項目を入力してください。");
            return;
        }
        const data = {
            UserId: 1,
            IsFound: false,
            LostDate: lostDate,
            FoundDate: foundDate,
            LostItem: lostItem,
            LostPlace: lostPlace,
            LostDetailedPlace: lostDetailedPlace,
        };
        yield fetch("/LostApi/InsertLost", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error("Failed to insert item");
                }
                return response.json();
            })
            .then(data => {
                console.log("Insert successful:", data);
            })
            .catch(error => {
                console.error("Error:", error);
            });
        location.reload();
    }));
});
// 更新ボタン押下時の処理
document.addEventListener("DOMContentLoaded", function () {
    var _a;
    (_a = document.getElementById("update-button")) === null || _a === void 0 ? void 0 : _a.addEventListener("click", () => __awaiter(this, void 0, void 0, function* () {
        //const selectedItems = getCheckLostId();
        //if (selectedItems.length === 0) {
        //    alert("更新するデータを選択してください。");
        //    return;
        //}
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
            // update.cshtmlに遷移
            window.location.href = "/Update";
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
let lostList = []; // 起動時に取得したデータを保持
// チェックボックスで選択された紛失物IDを取得する関数
function getCheckLostId() {
    // チェックボックスで選択された紛失物IDを取得
    const checkedLostIds = Array.from(document.querySelectorAll("input[type='checkbox']:checked"))
        .map((checkbox) => parseInt(checkbox.value));
    return checkedLostIds;
}
// テーブルを初期化し、新規で取得したデータを表示する関数
function initTable() {
    // テーブルを初期化
    const tableBody = document.querySelector("table tbody");
    if (tableBody) {
        tableBody.innerHTML = ""; // テーブルの内容をクリア
    }
}
function createTable(data) {
    // テーブルを初期化
    const tableBody = document.querySelector("table tbody");
    if (!tableBody)
        return;
    // cshtmlで埋め込んだベースURLを取得
    const config = document.getElementById("lost-config");
    // const baseUrl = config.dataset.detailUrl;
    const baseUrl = '/Home/Detail?lostId=1'
    // データをテーブルに追加
    data.forEach((item, index) => {
        const row = document.createElement("tr");
        // チェックボックス列
        const checkboxCell = document.createElement("td");
        const checkbox = document.createElement("input");
        checkbox.type = "checkbox";
        checkbox.value = item.lostId.toString();
        checkboxCell.appendChild(checkbox);
        row.appendChild(checkboxCell);
        // 紛失ID列（行番号を表示）
        const idCell = document.createElement("td");
        idCell.textContent = (index + 1).toString();
        row.appendChild(idCell);
        // 詳細列（●を表示）
        const form = document.createElement("form");
        form.method = "POST";
        form.action = "/Lost/DetailPost"; // POST用アクション

        const hiddenField = document.createElement("input");
        hiddenField.type = "hidden";
        hiddenField.name = "lostId";
        hiddenField.value = item.lostId.toString();
        form.appendChild(hiddenField);

        const detailCell = document.createElement("td");
        const btn = document.createElement("span");
        btn.textContent = "●";
        btn.style.cursor = "pointer";
        btn.addEventListener("click", () => __awaiter(this, void 0, void 0, function* () {
                if (item.lostId === -1 || item.lostId === null) {
                    alert("紛失IDが存在しません");
                    return;
                }
                else {
                    // サーバーに選択されたデータを送信
                    const url = `${baseUrl}` + `?lostId=${item.lostId}`;
                    window.location.href = url;
                }
            }));
            detailCell.appendChild(btn);
            row.appendChild(detailCell);
            // なくした日付列
            const lostDateCell = document.createElement("td");
            lostDateCell.textContent = item.lostDate ? formatDate(item.lostDate) : "";
            row.appendChild(lostDateCell);
            // 見つけた日付列
            const foundDateCell = document.createElement("td");
            foundDateCell.textContent = item.foundDate ? formatDate(item.foundDate) : "";
            row.appendChild(foundDateCell);
            // なくしたもの列
            const lostItemCell = document.createElement("td");
            lostItemCell.textContent = item.lostItem;
            row.appendChild(lostItemCell);
            // なくした場所列
            const lostPlaceCell = document.createElement("td");
            lostPlaceCell.textContent = item.lostPlace;
            row.appendChild(lostPlaceCell);
            // なくした詳細な場所列
            const lostDetailedPlaceCell = document.createElement("td");
            lostDetailedPlaceCell.textContent = item.lostDetailedPlace;
            row.appendChild(lostDetailedPlaceCell);
            // 行をテーブルに追加
            tableBody.appendChild(row);
        });
    }
// 日付を yyyy/MM/dd 形式にフォーマットする関数
function formatDate(dateString) {
            const date = new Date(dateString);
            return `${date.getFullYear()}/${(date.getMonth() + 1).toString().padStart(2, "0")}/${date.getDate().toString().padStart(2, "0")}`;
        }

function getLostTextValue() {
    const lostId = document.getElementById("lostId").value;
    const lostItem = document.getElementById("lostItem").value;
    const lostPlace = document.getElementById("lostPlace").value;
    const isFound = document.getElementById("isFound").checked;
    const lostDetailedPlace = document.getElementById("lostDetail").value;
    const lostDateValue = document.getElementById("lostDate").value ?? null;
    const foundDateValue = document.getElementById("foundDate").value ?? null;


    // ISO8601形式に変換（空の場合はnull）
    const lostDate = lostDateValue ? new Date(lostDateValue).toISOString() : null;
    const foundDate = foundDateValue ? new Date(foundDateValue).toISOString() : null;

    if (!lostItem || !lostPlace || !lostDetailedPlace) {
        alert("全ての項目を入力してください。");
        return;
    }

    // 日本時間でyyyy/MM/dd形式の文字列を生成
    const jpDate = new Date();
    const jpTime = jpDate.toLocaleString("ja-JP", { timeZone: "Asia/Tokyo" });
    const dateObj = new Date(jpTime);
    const updateDate = `${dateObj.getFullYear()}/${(dateObj.getMonth() + 1).toString().padStart(2, "0")}/${dateObj.getDate().toString().padStart(2, "0")}`;

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
        UpdateDate: updateDate // yyyy/MM/dd形式
    };

    return data;
}