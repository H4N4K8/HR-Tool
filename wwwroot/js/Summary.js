document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("addButton").addEventListener("click", function () {
        // Get selected values from the dropdowns
        var competencyID = document.getElementById("compt").value;
        var proficiencyID = document.getElementById("prof").value;

        // Check if both selections are made
        if (competencyID && proficiencyID) {
            // Find the names of the selected competency and proficiency
            var competencyName = getCompetencyNameById(competencyID);
            var proficiencyLevel = getProficiencyLevelById(proficiencyID);

            if (competencyName && proficiencyLevel) {
                // Create a new list item with the names
                var listItem = document.createElement("li");
                listItem.textContent = "Competency: " + competencyName + " | Proficiency: " + proficiencyLevel;

                // Append the list item to the unordered list
                document.getElementById("selectedItemsList").appendChild(listItem);

                // Optionally, clear the dropdowns after adding
                document.getElementById("compt").value = "";
                document.getElementById("prof").value = "";
            } else {
                alert("Could not find matching Competency or Proficiency.");
            }
        } else {
            alert("Please select both Competency and Proficiency.");
        }

        console.log("Selected Competency ID:", competencyID);
        console.log("Selected Proficiency ID:", proficiencyID);
        console.log("Matching Competency:", getCompetencyNameById(competencyID));
        console.log("Matching Proficiency:", getProficiencyLevelById(proficiencyID));

    });

    document.querySelector("form").addEventListener("submit", function () {
        const items = [];
        document.querySelectorAll("#selectedItemsList li").forEach(li => {
            const parts = li.textContent.split("|").map(p => p.trim());
            const competency = parts[0].replace("Competency: ", "").trim();
            const proficiency = parts[1].replace("Proficiency: ", "").trim();
            items.push({ competency, proficiency });
        });

        document.getElementById("SelectedItemsJson").value = JSON.stringify(items);
    });
});

function getCompetencyNameById(id) {
    var competency = competencies.find(item => item.value == id);
    return competency ? competency.text : null;
}

function getProficiencyLevelById(id) {
    var proficiency = proficiencies.find(item => item.value == id);
    return proficiency ? proficiency.text : null;
}