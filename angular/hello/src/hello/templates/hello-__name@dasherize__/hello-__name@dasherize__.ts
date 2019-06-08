@Component({
  selector: "hello-<%= dasherize(name) %>"
})
export class Hello<%= classify(name) %>Component {

}