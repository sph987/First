export const combineDateAndTime = (date:Date, time: Date) => {
    const timeString = time.getHours() + ':' + time.getMinutes() + ':00';

    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();
    const dateSTring = `${year}-${month}-${day}`;

    return new Date(dateSTring + ' ' + timeString);
}