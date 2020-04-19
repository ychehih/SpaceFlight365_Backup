/// <reference path="../../jQuery/jquery_1.9.1_vsdoc.js" />



$(function () {
 

 $("#btnSave").on("click", save);
 $("#btnCancel").on("click", cancel);

 if (typeof dialogArguments != "undefined" && typeof dialogArguments != null) {


   showRecords();
 
  
 }

});


function selectRow() {
 $(".sdk-sample-List-SelectedRow").removeClass("sdk-sample-List-SelectedRow").addClass("sdk-sample-List-Row");
 $(this).removeClass("sdk-sample-List-Row");
 $(this).addClass("sdk-sample-List-SelectedRow");
}

function showRecords() {
 var summaryMessage = "";
 var numberOfRecords = dialogArguments.length;
 var resultsTable = $("<table class='sdk-sample-List-Data' id='gridBodyTable' />");
 resultsTable.attr("border", 1);
 resultsTable.attr("cellspacing", 0);
 resultsTable.attr("cellpadding", 1);
 

 if (numberOfRecords > 1)
 { summaryMessage = "This list contains " + numberOfRecords + " Contact records." }
 else
 {
  summaryMessage = "This list contains 1 Contact record."
  var DlgHdTitleText = "Potential Duplicate Contact Detected";
  $("#DlgHdTitle").text(DlgHdTitleText);
  $("#DlgHdTitle").attr("title", DlgHdTitleText);
  var DlgHdDescText = "This contact may be a duplicate of the contact below."
  $("#DlgHdDesc").text(DlgHdDescText);
  $("#DlgHdDesc").attr("title", DlgHdDescText);

 }
 resultsTable.attr("summary", summaryMessage);

 var colGroup = $("<colgroup />");
 colGroup.append($("<col width='200' class='sdk-sample-List-DataColumn' name='fullname' />"));
 colGroup.append($("<col width='200' class='sdk-sample-List-DataColumn' name='emailaddress1' />"));
 resultsTable.append(colGroup);

 for (var i = 0; i < dialogArguments.length; i++) {
  var record = dialogArguments[i];
  var row = $("<tr class='sdk-sample-List-Row' otypename='contact' oid=" + record.id + " />");
  //fullName column
  var fullnameCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var fullnameGridcellpadding = $("<nobr class='gridcellpadding' />");
  var fullnameLink = $("<a class='sdk-sample-List-Link' />");
  fullnameLink.attr("title", record.fullname)
  fullnameLink.attr("tabindex", i)
  fullnameLink.attr("href", "#")
  fullnameLink.attr("target", "_blank")
  fullnameLink.attr("oid", record.id)
  fullnameLink.attr("otypename", 'contact')

  if (typeof record.fullname != "undefined") {
   // Quick Create form can't have fullname in the attributes
   fullnameLink.text(record.fullname);
  }
  else {
   fullnameLink.text(record.firstname +" "+record.lastname);
  }
  

  fullnameGridcellpadding.append(fullnameLink);
  fullnameCell.append(fullnameGridcellpadding);
  fullnameCell.append(fullnameGridcellpadding);
  row.append(fullnameCell);
  fullnameLink.on("click", openRecord);

  //emailaddress1 column
  var emailaddress1Cell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var emailaddress1Gridcellpadding = $("<nobr class='gridcellpadding' />");
  emailaddress1Gridcellpadding.attr("title", record.emailaddress1)
  emailaddress1Gridcellpadding.text(record.emailaddress1);
  emailaddress1Cell.append(emailaddress1Gridcellpadding);
  row.append(emailaddress1Cell);

  //spacer Column
  row.append($("<td class='sdk-sample-List-DataCell'><nobr class='gridcellpadding'></nobr></td>"));

  row.on("click", selectRow);
  row.on("dblclick", openRecord);

  resultsTable.append(row);

 }
 $("#DlgHdBodyContainer").append(resultsTable);

}

function openRecord() {
 var type = $(this).attr("otypename");
 var id = $(this).attr("oid");
 window.returnValue = { "type": type, "id": id };
 window.close();
 return false;
}

 function save() {
  window.returnValue = "save";
  window.close();
 }
 function cancel() {
  window.returnValue = "cancel";
  window.close();
 }