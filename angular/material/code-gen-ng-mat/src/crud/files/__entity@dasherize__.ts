export class <%= classify(entity) %> {<% for (let field of model.fields) { %>
  <%=field.name%>: <%=field.type%>;<% } %>
}
