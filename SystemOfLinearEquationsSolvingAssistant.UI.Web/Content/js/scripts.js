const MIN_DATA_DIMENSION = 1;
const MAX_DATA_DIMENSION = 999;

let inputDataErrorCells = [];

document.addEventListener("DOMContentLoaded", () => {
    const tableMatrixA = document.getElementById("input-data-matrix-a");
    const tdMatrixA = document.getElementById("input-data-matrix-a").getElementsByTagName("td");
    const tdVectorB = document.getElementById("input-data-vector-b").getElementsByTagName("td");

    let currentDataDimension = tableMatrixA.rows.length - 1;

    [...tdMatrixA].forEach(td => {
        addInputDataCellBehavior(td);
    });

    [...tdVectorB].forEach(td => {
        addInputDataCellBehavior(td);
    });

    updateInputActionsState(currentDataDimension);
});

function addInputDataCellBehavior(td) {
    td.addEventListener("focusin", _event => {
        const tdTextRange = document.createRange();
        tdTextRange.selectNodeContents(td);

        const currentSelection = document.getSelection();
        currentSelection.removeAllRanges();
        currentSelection.addRange(tdTextRange);
    });

    td.addEventListener("focusout", _event => {
        const tdNumText = Number(td.innerText);

        if (isNaN(tdNumText) === true) {
            if (inputDataErrorCells.includes(td) === false) {
                inputDataErrorCells.push(td);
            }

            const divInputDataError = document.createElement("div");
            divInputDataError.className = "input-data-error-div";
            divInputDataError.innerText = td.innerText;

            td.innerHTML = "";
            td.appendChild(divInputDataError);
        }
        else if (inputDataErrorCells.includes(td) === true) {
            inputDataErrorCells.splice(inputDataErrorCells.indexOf(td), 1);

            const tdText = td.innerText;
            td.innerHTML = "";
            td.innerText = tdText;
        }

        updateSolvingActionsState();
    });
}

function changeDataDimension(dimensionsCount, isExpanding) {
    const tableMatrixA = document.getElementById("input-data-matrix-a");
    const tableVectorB = document.getElementById("input-data-vector-b");

    let currentDataDimension = tableMatrixA.rows.length - 1;

    if (((isExpanding === true) && ((currentDataDimension + dimensionsCount) > MAX_DATA_DIMENSION)) ||
        ((isExpanding === false) && ((currentDataDimension - dimensionsCount) < MIN_DATA_DIMENSION))) {
        return;
    }

    if (isExpanding === true) {
        const thColumnPrototype = tableMatrixA.rows[0].getElementsByTagName("th")[1];
        const thRowPrototype = tableMatrixA.rows[1].getElementsByTagName("th")[0];
        const tdPrototype = tableMatrixA.rows[1].getElementsByTagName("td")[0];
        let tdNew;

        for (let i = 0; i < dimensionsCount; i++) {
            currentDataDimension++;

            tableMatrixA.rows[0].appendChild(createTag("th", currentDataDimension, thColumnPrototype));

            for (let j = 1; j < currentDataDimension; j++) {
                tdNew = createTag("td", 0, tdPrototype);
                addInputDataCellBehavior(tdNew);

                tableMatrixA.rows[j].appendChild(tdNew);
            }

            const trMatrixA = tableMatrixA.insertRow(-1);
            trMatrixA.appendChild(createTag("th", currentDataDimension, thRowPrototype));

            for (let j = 0; j < currentDataDimension; j++) {
                tdNew = createTag("td", 0, tdPrototype);
                addInputDataCellBehavior(tdNew);

                trMatrixA.appendChild(tdNew);
            }

            const trVectorB = tableVectorB.insertRow(-1);
            trVectorB.appendChild(createTag("th", currentDataDimension, thRowPrototype));

            tdNew = createTag("td", 0, tdPrototype);
            addInputDataCellBehavior(tdNew);

            trVectorB.appendChild(tdNew);
        }
    }
    else {
        inputDataErrorCells = inputDataErrorCells.filter(td => {
            return (td.closest("tr").rowIndex !== currentDataDimension) && (td.cellIndex !== currentDataDimension);
        });

        for (let i = 0; i < dimensionsCount; i++) {
            for (let j = 0; j < currentDataDimension; j++) {
                tableMatrixA.rows[j].deleteCell(currentDataDimension);
            }

            tableMatrixA.deleteRow(currentDataDimension);
            tableVectorB.deleteRow(currentDataDimension);

            currentDataDimension--;
        }
    }

    updateInputActionsState(currentDataDimension);
    updateSolvingActionsState();
}

function resetData(dimensionsCount) {
    clearData();
    changeDataDimension(dimensionsCount - 1, true);
}

async function solveSerial() {
    if (inputDataErrorCells.length !== 0) {
        return;
    }

    const selectAlgorithmNamesSerial = document.getElementById("algorithm-names-serial");

    const sole = getSoleFromData();
    const solvingAlgorithm = selectAlgorithmNamesSerial.options[selectAlgorithmNamesSerial.selectedIndex].text;

    const request = new Request("/", {
        method: "POST",
        body: JSON.stringify({ "a": sole.a, "b": sole.b, "solvingAlgorithm": solvingAlgorithm })
    });

    lockUserControls();

    await fetch(request)
        .then(() => {
            location.reload();
        });
}

async function solveParallel() {
    if (inputDataErrorCells.length !== 0) {
        return;
    }

    const selectAlgorithmNamesParallel = document.getElementById("algorithm-names-parallel");
    const inputThreadsNumParallel = document.getElementById("threads-num-parallel");

    const threadsNum = Math.floor(Number(inputThreadsNumParallel.value));

    if ((threadsNum < 2) || (threadsNum > 99)) {
        alert("The number of threads must be in the range from 2 to 99.");

        return;
    }

    const sole = getSoleFromData();
    const solvingAlgorithm = selectAlgorithmNamesParallel.options[selectAlgorithmNamesParallel.selectedIndex].text;

    const request = new Request("/", {
        method: "POST",
        body: JSON.stringify({
            "a": sole.a,
            "b": sole.b,
            "solvingAlgorithm": solvingAlgorithm,
            "threadsNum": threadsNum
        })
    });

    lockUserControls();

    await fetch(request)
        .then(() => {
            location.reload();
        });
}

function clearData() {
    const tableMatrixA = document.getElementById("input-data-matrix-a");
    const tableVectorB = document.getElementById("input-data-vector-b");

    let currentDataDimension = tableMatrixA.rows.length - 1;

    while (currentDataDimension !== 1) {
        changeDataDimension(1, false);
        currentDataDimension = tableMatrixA.rows.length - 1;
    }

    tableMatrixA.rows[1].getElementsByTagName("td")[0].innerHTML = 0;
    tableVectorB.rows[1].getElementsByTagName("td")[0].innerHTML = 0;

    inputDataErrorCells = [];

    updateSolvingActionsState();
}

function getSoleFromData() {
    const tableMatrixA = document.getElementById("input-data-matrix-a");
    const tableVectorB = document.getElementById("input-data-vector-b");

    const a = [];
    const b = [];

    for (let i = 1; i < tableMatrixA.rows.length; i++) {
        let rowTexts = [];

        for (let j = 1; j < tableMatrixA.rows.length; j++) {
            rowTexts.push(tableMatrixA.rows[i].cells[j].innerText);
        }

        a.push(rowTexts);
        b.push(tableVectorB.rows[i].cells[1].innerText);
    }

    return { "a": a, "b": b };
}

function updateInputActionsState(currentDataDimension) {
    const buttonAddDataDimension = document.getElementById("add-data-dimension");
    const buttonRemoveDataDimension = document.getElementById("remove-data-dimension");

    switch (currentDataDimension) {
        case MIN_DATA_DIMENSION:
            buttonAddDataDimension.disabled = false
            buttonRemoveDataDimension.disabled = true;
            break;
        case MAX_DATA_DIMENSION:
            buttonAddDataDimension.disabled = true
            buttonRemoveDataDimension.disabled = false;
            break;
        default:
            buttonAddDataDimension.disabled = false
            buttonRemoveDataDimension.disabled = false;
            break;
    }
}

function updateSolvingActionsState() {
    const buttonSolveSerial = document.getElementById("solve-serial");
    const buttonSolveParallel = document.getElementById("solve-parallel");

    if (inputDataErrorCells.length !== 0) {
        buttonSolveSerial.disabled = true;
        buttonSolveParallel.disabled = true;
    }
    else {
        buttonSolveSerial.disabled = false;
        buttonSolveParallel.disabled = false;
    }
}

function lockUserControls() {
    const thMatrixA = document.getElementById("input-data-matrix-a").getElementsByTagName("th");
    const tdMatrixA = document.getElementById("input-data-matrix-a").getElementsByTagName("td");
    const thVectorB = document.getElementById("input-data-vector-b").getElementsByTagName("th");
    const tdVectorB = document.getElementById("input-data-vector-b").getElementsByTagName("td");

    [...thMatrixA].forEach(th => {
        th.style.cursor = "default";
    });

    [...tdMatrixA].forEach(td => {
        td.setAttribute("contenteditable", "false");
    });

    [...thVectorB].forEach(th => {
        th.style.cursor = "default";
    });

    [...tdVectorB].forEach(td => {
        td.setAttribute("contenteditable", "false");
    });

    document.getElementById("add-data-dimension").disabled = true;
    document.getElementById("remove-data-dimension").disabled = true;
    document.getElementById("reset-data").disabled = true;
    document.getElementById("algorithm-names-serial").disabled = true;
    document.getElementById("solve-serial").disabled = true;
    document.getElementById("elapsed-time-serial").disabled = true;
    document.getElementById("algorithm-names-parallel").disabled = true;
    document.getElementById("threads-num-parallel").disabled = true;
    document.getElementById("solve-parallel").disabled = true;
    document.getElementById("elapsed-time-parallel").disabled = true;
    document.getElementById("solution-set").disabled = true;
}

function createTag(name, value, prototype) {
    const tag = document.createElement(name);
    tag.innerHTML = value;

    if (prototype !== undefined) {
        [...prototype.attributes].forEach(a => {
            if (a.name !== "id") {
                tag.setAttribute(a.name, a.value);
            }
        });
    }

    return tag;
}