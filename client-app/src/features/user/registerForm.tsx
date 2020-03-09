import React, { useContext } from "react";
import { Form as FinalForm, Field } from "react-final-form";
import { Form, Button,  Header } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import { IUserFormValues } from "../../app/models/user";
import { TextInput } from "../../app/common/form/TextInput";
import { FORM_ERROR } from "final-form";
import { combineValidators, isRequired } from "revalidate";
import { ErrorMessage } from "../../app/common/form/ErrorMessage";

const validate = combineValidators({
  username: isRequired("username"),
  displayName: isRequired("display name"),
  email: isRequired("email"),
  password: isRequired("password"),

});

export const RegisterForm = () => {
  const rootStore = useContext(RootStoreContext);
  const { register } = rootStore.userStore;
  return (
    <FinalForm
      onSubmit={(values: IUserFormValues) =>
        register(values).catch(error => ({
          [FORM_ERROR]: error
        }))
      }
      validate={validate}
      render={({
        handleSubmit,
        submitting,
        submitError,
        invalid,
        pristine,
        dirtySinceLastSubmit
      }) => (
        <Form onSubmit={handleSubmit} error>
          <Header
            as="h2"
            content="Sign up to Reactivities"
            color="teal"
            textAlign="center"
            fluid
          />
          <Field
            name="username"
            component={TextInput}
            type="text"
            placeholder="Username"
          />
          <Field
            name="displayName"
            component={TextInput}
            type="text"
            placeholder="Display Name"
          />
          <Field
            name="email"
            component={TextInput}
            type="text"
            placeholder="Email"
          />
          <Field
            name="password"
            component={TextInput}
            placeholder="Password"
            type="password"
          />
          {submitError && !dirtySinceLastSubmit && (
            <ErrorMessage
              error={submitError}
              text= ''
              //text={JSON.stringify(submitError.data.errors)}
            />
          )}
          <Button
            disabled={(invalid && !dirtySinceLastSubmit) || pristine}
            loading={submitting}
            color="teal"
            content="Register"
            fluid
          />
          {/* <pre>{JSON.stringify(form.getState(), null, 2)}</pre> */}
        </Form>
      )}
    />
  );
};
