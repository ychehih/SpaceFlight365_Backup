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
 { summaryMessage = "This list contains " + numberOfRecords + " Lead records." }
 else
 {
  summaryMessage = "This list contains 1 Lead record."
  var DlgHdTitleText = "Potential Duplicate Lead Detected";
  $("#DlgHdTitle").text(DlgHdTitleText);
  $("#DlgHdTitle").attr("title", DlgHdTitleText);
  var DlgHdDescText = "This lead may be a duplicate of the lead below."
  $("#DlgHdDesc").text(DlgHdDescText);
  $("#DlgHdDesc").attr("title", DlgHdDescText);

 }
 resultsTable.attr("summary", summaryMessage);

 var colGroup = $("<colgroup />");
 colGroup.append($("<col width='175' class='sdk-sample-List-DataColumn' name='topic' />"));
 colGroup.append($("<col width='175' class='sdk-sample-List-DataColumn' name='fullname' />"));
 colGroup.append($("<col width='175' class='sdk-sample-List-DataColumn' name='address1_city' />"));
 colGroup.append($("<col width='175' class='sdk-sample-List-DataColumn' name='emailaddress1' />"));
 resultsTable.append(colGroup);

 for (var i = 0; i < dialogArguments.length; i++) {
  var record = dialogArguments[i];
  var row = $("<tr class='sdk-sample-List-Row' otypename='lead' oid=" + record.id + " />");


  //subject column
  var subjectCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var subjectGridcellpadding = $("<nobr class='gridcellpadding' />");
  var subjectLink = $("<a class='sdk-sample-List-Link' />");
  subjectLink.attr("title", record.subject)
  subjectLink.attr("tabindex", i)
  subjectLink.attr("href", "#")
  subjectLink.attr("target", "_blank")
  subjectLink.attr("oid", record.id)
  subjectLink.attr("otypename", 'lead')
  subjectLink.text(record.subject);
  subjectGridcellpadding.append(subjectLink);
  subjectCell.append(subjectGridcellpadding);
  subjectCell.append(subjectGridcellpadding);
  row.append(subjectCell);
  subjectLink.on("click", openRecord);


  //fullname column
  var fullnameCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var fullnameGridcellpadding = $("<nobr class='gridcellpadding' />");
  fullnameGridcellpadding.attr("title", record.fullname)
  if (typeof record.fullname != "undefined")
  {
   fullnameGridcellpadding.text(record.fullname);
  }
  else
  {
   fullnameGridcellpadding.text(record.firstname + " " + record.lastname);
  }
  fullnameCell.append(fullnameGridcellpadding);
  row.append(fullnameCell);



  //address1_city column
  var address1_cityCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var address1_cityGridcellpadding = $("<nobr class='gridcellpadding' />");
  address1_cityGridcellpadding.attr("title", record.address1_city)
  address1_cityGridcellpadding.text(record.address1_city);
  address1_cityCell.append(address1_cityGridcellpadding);
  row.append(address1_cityCell);


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