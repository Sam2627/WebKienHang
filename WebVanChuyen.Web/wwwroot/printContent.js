window.printContent = () => {
    var divcontent = document.getElementById("PrintDiv").innerHTML;
    var print = window.open('', '', 'height=600, width=600');
    print.document.write('<html>');
    print.document.write('<body><br/>');
    print.document.write(divcontent);
    print.document.write()
    print.document.write('</body></html>');
    print.document.close();
    print.print();
}