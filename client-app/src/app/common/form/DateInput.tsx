import React from 'react'
import { FieldRenderProps } from "react-final-form";
import { FormFieldProps, Form, Label } from "semantic-ui-react";
import {DateTimePicker} from 'react-widgets';

interface IProps
  extends FieldRenderProps<Date, HTMLElement>,
    FormFieldProps {}

export const DateInput:React.FC<IProps > = ({
    input,
    width,
    placeholder,
    date = false,
    time = false,
    meta: { touched, error },
    ...rest
  }) => {
    return (
        <Form.Field error={touched && !!error} width={width}>
            <DateTimePicker 
                placeholder={placeholder}
                value = {input.value || null}
                onChange={input.onChange}
                date={date}
                time = {time}
                //{...rest}
                //type={rest.type}

            />
        {touched && error && (
            <Label basis color='red'>
                {error}
            </Label>
        )}
    </Form.Field>
    )
}