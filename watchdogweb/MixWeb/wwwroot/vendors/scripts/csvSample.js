// 當文件載入完成後執行
$(document).ready(function() {
    // 下載CSV檔案
    $.ajax({
      url: "sample.csv",
      dataType: "text",
      success: function(data) {
        var lines = data.split("\n");
        // var headers = lines[0].split(",");
        var headers =[{title:'編號'},{title:'檢測網域'},{title:'狀態'},{title:'檢測時間'},{title:'商戶'}];
        // var dataArray = [['Tiger Nixon', 'System Architect', 'Edinburgh', '5421']];
        var dataArray = [];
        for (var i = 1; i < lines.length-1; i++) {
          var dataLine = lines[i].split(",");
          //var dataObject = {};
          var dataObject = [];
          dataObject.push(i);
          for (var j = 0; j < headers.length; j++) {
            //dataObject[headers[j]] = dataLine[j];
            dataObject.push(dataLine[j]);
          }
          dataArray.push(dataObject);
        }
        $('#myTable').DataTable({
          data: dataArray,
          columns: headers
        });
      }
      // success: function(data) {
      //   var headers =[{title:'檢測網域'},{title:'狀態'},{title:'檢測時間'},{title:'商戶'}];
      //   // 使用Papa Parse套件解析CSV資料
      //   Papa.parse(data, {
      //     header: false,
      //     //encoding: "utf-8", // 加上 UTF-8 編碼
      //     skipEmptyLines: true,
      //     complete: function(results) {
      //       // 使用DataTable套件填入表格
      //       var titles = results.data.shift();
      //       var cols=[];
      //       for (var i=0; i< titles.lenght-1; i++){
      //           var obj ={};
      //           obj["title"]=titles[i];
      //           cols.push(obj);
      //       }
      //       $('#myTable').DataTable({
      //         data: results.data,
      //         // columns: Object.keys(results.data[0])
      //         columns: headers
      //       });
      //     }
      //   });
      // }
    });
  });