export class <%= classify(entity.name) %> {<% for (let field of entity.fields) { %>
  <%=field.name%>: <%=field.dataType%>;<% } %>
}
