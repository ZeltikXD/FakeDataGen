const INT_MIN = -2147483648;
const INT_MAX = 2147483647;

document.addEventListener('DOMContentLoaded', () => {
    document.getElementById('generate_csv').addEventListener('click', () => {
        // Variable to store the final csv data
        let csv_data = [];

        // Get each row data
        const rows = document.getElementsByTagName('tr');
        for (let i = 0; i < rows.length; i++) {

            // Get each column data
            const cols = rows[i].querySelectorAll('td,th');

            // Stores each csv row data
            const csvrow = [];
            for (let j = 0; j < cols.length; j++) {
                // Get the text data of each cell
                let cellData = cols[j].innerText;

                // If the cell contains a comma or double quote, handle it
                if (cellData.includes(',') || cellData.includes('"')) {
                    // Escape double quotes by replacing " with ""
                    cellData = `"${cellData.replace(/"/g, '""')}"`;
                }

                // Push the properly formatted cell data to csvrow
                csvrow.push(cellData);
            }

            // Combine each column value with a comma
            csv_data.push(csvrow.join(","));
        }

        // Combine each row data with a new line character
        csv_data = csv_data.join('\n');

        // Download the CSV file
        downloadCSVFile(csv_data);
    });

    function downloadCSVFile(csv_data) {
        const temp_link = document.createElement('a');

        temp_link.download = "exported.csv";
        temp_link.href = 'data:text/csv;charset=utf-8,%EF%BB%BF' + encodeURI(csv_data);

        temp_link.style.display = "none";
        document.body.appendChild(temp_link);

        temp_link.click();
        document.body.removeChild(temp_link);
    }

    const rangeInput = document.getElementById('err-range');
    const numberInput = document.getElementById('err-input');
    const locSelect = document.getElementById('loc-select');
    const seedInput = document.getElementById('seed');

    document.getElementById('generate_seed').addEventListener('click', () => {
        seedInput.value = getRandomInt32();
        seedInput.dispatchEvent(new Event('change', { cancelable: false }));
    });

    rangeInput.addEventListener('input', () => {
        numberInput.value = rangeInput.value;
    });

    numberInput.addEventListener('input', () => {
        rangeInput.value = numberInput.value;
    });

    rangeInput.addEventListener('change', obtainNewData);
    numberInput.addEventListener('change', obtainNewData);
    locSelect.addEventListener('change', obtainNewData);
    seedInput.addEventListener('change', obtainNewData);

    let currentPage = 1;
    const pageSize = 20;
    let isLoading = false;
    let allDataLoaded = false;

    const container = document.getElementById('main-table');

    loadMoreData();

    window.addEventListener('scroll', () => {
        if (window.scrollY + window.innerHeight >= container.offsetTop + container.offsetHeight) {
            if (!isLoading && !allDataLoaded) {
                loadMoreData();
            }
        }
    });

    function loadMoreData() {
        isLoading = true;
        const seed = seedInput.value;
        const errorProb = numberInput.value;
        const locale = locSelect.value;

        fetch(`/api/persons?page=${currentPage}&size=${pageSize}&seed=${seed}&errorProb=${errorProb}&locale=${locale}`)
            .then(response => response.json())
            .then(data => {
                if (data.length === 0) {
                    allDataLoaded = true;
                } else {
                    appendRows(data);
                    currentPage++;
                }
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            })
            .finally(() => {
                isLoading = false;
            });
    }

    function obtainNewData() {
        currentPage = 1;
        refreshTable();
        loadMoreData();
    }

    function appendRows(data) {
        const tableBody = container.querySelector('tbody');
        data.forEach(row => {
            const newRow = document.createElement('tr');
            newRow.innerHTML = `
            <td>${row.index + 1}</td>
            <td>${row.identifier}</td>
            <td>${row.fullName}</td>
            <td>${row.fullAddress}</td>
            <td>${row.phoneNumber}</td>
        `;
            tableBody.appendChild(newRow);
        });
    }

    function refreshTable() {
        const tableBody = container.querySelector('tbody');
        const trList = tableBody.querySelectorAll('tr');
        trList.forEach((element) => {
            tableBody.removeChild(element);
        });
    }

    function getRandomInt32() {
        return Math.floor(Math.random() * (INT_MAX - INT_MIN + 1)) + INT_MIN;
    }
});